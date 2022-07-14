using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace BlaggoBlackbox
{
    public class Blaggo
    {
        private Options options;

        public Blaggo(Options options)
        {
            var authenticator = options.AuthenticatorFn;
            if (options.HttpClient == null)
            {
                options.HttpClient = new HttpClient();
            }

            if (authenticator == null)
            {
                options.AuthenticatorFn = AuthFn;
            }
            this.options = options;
        }

        public async Task<AuthResponse> AuthFn(string url, Credentials creds)
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
    }
}