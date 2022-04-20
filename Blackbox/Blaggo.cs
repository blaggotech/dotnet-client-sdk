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

        public Blaggo(string baseUrl, string username, string password)
        {
            this.url = baseUrl;
            this.username = username;
            this.password = password;
        }

        public async Task<AuthResponse> GetAuthToken()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), this.url))
                {

                    LoginPayload payload = new LoginPayload
                    {
                        Username = this.username,
                        Password = this.password
                    };

                    var json = JsonConvert.SerializeObject(payload, Formatting.Indented);

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);

                    response.EnsureSuccessStatusCode(); // throws if not 200-299
                    var contentString = response.Content.ReadAsStringAsync();

                    AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(await contentString);

                    return authResponse;
                }
            }
        }
    }
}