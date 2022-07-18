using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace BlaggoBlackbox
{
    public class Blackbox
    {
        private static readonly string? BLACKBOX_BASE_URL = Environment.GetEnvironmentVariable("BLACKBOX_BASE_URL");
        private Options options;

        public Blackbox(Options options)
        {
            var authenticator = options.AuthenticatorFn;
            if (options.HttpClient == null)
            {
                options.HttpClient = new HttpClient();
            }

            if (authenticator == null)
            {
                options.AuthenticatorFn = Authenticate;
            }
            this.options = options;
        }

        public async Task<AuthResponse> Authenticate(string url, Credentials creds)
        {
            LoginPayload payload = new LoginPayload
            {
                Username = creds.Username,
                Password = creds.Password,
            };

            const string contentType = "application/json";

            var json = JsonConvert.SerializeObject(payload, Formatting.Indented);
            var response = await this.options.HttpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, contentType));

            response.EnsureSuccessStatusCode(); // throws if not 200-299.
            var contentString = response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AuthResponse>(await contentString);
        }

        public async Task<PayloadResponse?> GetPayloads()
        {
            var authResponse = await this.options.AuthenticatorFn(this.options.AuthURL, this.options.Credentials);
            var accessToken = authResponse.Data.Tokens.AccessToken;

            var payloadsUrl = BLACKBOX_BASE_URL + "/payloads";
            var uri = new Uri(payloadsUrl);

            var httpClient = this.options.HttpClient ?? new HttpClient();

            // add Authorization header
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponse = await httpClient.GetAsync(uri);

            httpResponse.EnsureSuccessStatusCode();

            var contentStream = await httpResponse.Content.ReadAsStringAsync();

            PayloadResponse? payloadResponse = JsonConvert.DeserializeObject<PayloadResponse>(contentStream);
            return payloadResponse;
        }

        public async Task<GetPayloadResponse?> GetPayload(string payloadId)
        {
            var authResponse = await this.options.AuthenticatorFn(this.options.AuthURL, this.options.Credentials);
            var accessToken = authResponse.Data.Tokens.AccessToken;

            var payloadsUrl = BLACKBOX_BASE_URL + "/payloads";
            var getPayloadByIDUrl = Path.Combine(payloadsUrl, payloadId);
            var uri = new Uri(getPayloadByIDUrl);

            var httpClient = this.options.HttpClient ?? new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponse = await httpClient.GetAsync(uri);

            httpResponse.EnsureSuccessStatusCode();

            var contentStream = await httpResponse.Content.ReadAsStringAsync();

            GetPayloadResponse? response = JsonConvert.DeserializeObject<GetPayloadResponse>(contentStream);
            return response;
        }
    }
}
