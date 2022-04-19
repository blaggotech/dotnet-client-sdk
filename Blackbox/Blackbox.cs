using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Blackbox
{
    public class Blackbox
    {
        private string accessToken;

        public Blackbox(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public async Task<SubscriberResponse> GetSubscribers(string subscriberURL)
        {
            using (var httpClient = new HttpClient())
            {
                var uri = new Uri(subscriberURL);

                // add Authorization header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.accessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var httpResponse = await httpClient.GetAsync(uri);

                httpResponse.EnsureSuccessStatusCode();

                var contentStream = await httpResponse.Content.ReadAsStringAsync();

                SubscriberResponse subscriberResponse = JsonConvert.DeserializeObject<SubscriberResponse>(contentStream);
                return subscriberResponse;
            }
        }
    }
}
