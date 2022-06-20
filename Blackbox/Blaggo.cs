using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Blackbox
{
    public class Blaggo
    {

        private string url;
        private string username;
        private string password;
        // private readonly HttpClient httpClient = new HttpClient();

        public Blaggo(string baseUrl, string username, string password)
        {
            this.url = baseUrl;
            this.username = username;
            this.password = password;
        }

        public async Task<AuthResponse?> GetAuthToken(HttpClient httpClient)
        {
            LoginPayload payload = new LoginPayload
            {
                Username = this.username,
                Password = this.password
            };

            var json = JsonConvert.SerializeObject(payload, Formatting.Indented);
            var response = await httpClient.PostAsync(this.url, new StringContent(json, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode(); // throws if not 200-299
            var contentString = response.Content.ReadAsStringAsync();

            AuthResponse? authResponse = JsonConvert.DeserializeObject<AuthResponse>(await contentString);

            return authResponse;
        }
    }
}