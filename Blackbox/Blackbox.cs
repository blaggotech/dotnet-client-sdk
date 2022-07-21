using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Blaggo.Blackbox
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

            var httpClient = this.options.HttpClient;

            // add Authorization header
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponse = await httpClient.GetAsync(uri);

            httpResponse.EnsureSuccessStatusCode();

            var contentStream = await httpResponse.Content.ReadAsStringAsync();

            PayloadResponse? payloadResponse = JsonConvert.DeserializeObject<PayloadResponse>(contentStream);
            return payloadResponse;
        }

        public async Task<InboxResponse?> GetInbox()
        {
            var authResponse = await this.options.AuthenticatorFn(this.options.AuthURL, this.options.Credentials);
            var accessToken = authResponse.Data.Tokens.AccessToken;

            var inboxUrl = BLACKBOX_BASE_URL + "/inbox";
            var uri = new Uri(inboxUrl);

            var httpClient = this.options.HttpClient;

            // add Authorization header
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponse = await httpClient.GetAsync(uri);

            httpResponse.EnsureSuccessStatusCode();

            var contentStream = await httpResponse.Content.ReadAsStringAsync();

            InboxResponse? inboxResponse = JsonConvert.DeserializeObject<InboxResponse>(contentStream);
            return inboxResponse;
        }

        public async Task DeleteInboxById(string inboxId)
        {
            var authResponse = await this.options.AuthenticatorFn(this.options.AuthURL, this.options.Credentials);
            var accessToken = authResponse.Data.Tokens.AccessToken;

            var subscribersUrl = BLACKBOX_BASE_URL + "/inbox?id=" + inboxId;
            var httpClient = this.options.HttpClient;

            using (var request = new HttpRequestMessage(HttpMethod.Delete, subscribersUrl))
            {
                request.Headers.Add("Authorization", "Bearer " + accessToken);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode(); // throws if not 200-299
            }
        }
        public async Task<SubscriberResponse> GetSubscribers()
        {
            var authResponse = await this.options.AuthenticatorFn(this.options.AuthURL, this.options.Credentials);
            var accessToken = authResponse.Data.Tokens.AccessToken;

            var subscribersUrl = BLACKBOX_BASE_URL + "/accounts";
            var uri = new Uri(subscribersUrl);

            var httpClient = this.options.HttpClient;
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponse = await httpClient.GetAsync(uri);

            httpResponse.EnsureSuccessStatusCode();

            var contentStream = await httpResponse.Content.ReadAsStringAsync();

            SubscriberResponse? subscriberResponse = JsonConvert.DeserializeObject<SubscriberResponse>(contentStream);
            return subscriberResponse;
        }

        public async Task DeleteSubscriber(string subscriberID)
        {
            var authResponse = await this.options.AuthenticatorFn(this.options.AuthURL, this.options.Credentials);
            var accessToken = authResponse.Data.Tokens.AccessToken;

            var subscribersUrl = BLACKBOX_BASE_URL + "/accounts";
            var deleteSubscriberURL = Path.Combine(subscribersUrl, subscriberID);
            var httpClient = this.options.HttpClient;

            using (var request = new HttpRequestMessage(HttpMethod.Delete, deleteSubscriberURL))
            {
                request.Headers.Add("Authorization", "Bearer " + accessToken);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode(); // throws if not 200-299
            }
        }

        public async Task DeleteProtocolPayload(string protocolId)
        {
            var authResponse = await this.options.AuthenticatorFn(this.options.AuthURL, this.options.Credentials);
            var accessToken = authResponse.Data.Tokens.AccessToken;

            var subscribersUrl = BLACKBOX_BASE_URL + "/payloads?id=" + protocolId;
            var httpClient = this.options.HttpClient;

            using (var request = new HttpRequestMessage(HttpMethod.Delete, subscribersUrl))
            {
                request.Headers.Add("Authorization", "Bearer " + accessToken);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode(); // throws if not 200-299
            }
        }

        public async Task<GetPayloadResponse?> GetPayload(string payloadId)
        {
            var authResponse = await this.options.AuthenticatorFn(this.options.AuthURL, this.options.Credentials);
            var accessToken = authResponse.Data.Tokens.AccessToken;

            var payloadsUrl = BLACKBOX_BASE_URL + "/payloads";
            var getPayloadByIDUrl = Path.Combine(payloadsUrl, payloadId);
            var uri = new Uri(getPayloadByIDUrl);

            var httpClient = this.options.HttpClient;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponse = await httpClient.GetAsync(uri);

            httpResponse.EnsureSuccessStatusCode();

            var contentStream = await httpResponse.Content.ReadAsStringAsync();

            GetPayloadResponse? response = JsonConvert.DeserializeObject<GetPayloadResponse>(contentStream);
            return response;
        }

        public async Task<GetPayloadResponse> QueryProtocolPayloads(PayloadOptions options)
        {
            var authResponse = await this.options.AuthenticatorFn(this.options.AuthURL, this.options.Credentials);
            var accessToken = authResponse.Data.Tokens.AccessToken;

            var payloadsUrl = getPayloadsURL(options);
            var uri = new Uri(payloadsUrl);

            var httpClient = this.options.HttpClient;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponse = await httpClient.GetAsync(uri);

            httpResponse.EnsureSuccessStatusCode();

            var contentStream = await httpResponse.Content.ReadAsStringAsync();

            GetPayloadResponse? response = JsonConvert.DeserializeObject<GetPayloadResponse>(contentStream);
            return response;
        }

        public string getPayloadsURL(PayloadOptions options)
        {
            return BLACKBOX_BASE_URL + $"/payloads?id={options.Id}&ids={options.Ids}&status={options.Status}&page={options.Page}&per_page={options.PerPage}";
        }
    }
}
