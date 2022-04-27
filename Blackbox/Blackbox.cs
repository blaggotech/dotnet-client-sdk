using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Blackbox
{
    public class Blackbox
    {
        private string accessToken;
        private string uri;

        public Blackbox(string uri, string accessToken)
        {
            this.uri = uri;
            this.accessToken = accessToken;
        }

        public async Task<PayloadResponse> GetPayloads()
        {
            using (var httpClient = new HttpClient())
            {
                var uri = new Uri(this.uri);

                // add Authorization header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.accessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var httpResponse = await httpClient.GetAsync(uri);

                httpResponse.EnsureSuccessStatusCode();

                var contentStream = await httpResponse.Content.ReadAsStringAsync();

                PayloadResponse payloadResponse = JsonConvert.DeserializeObject<PayloadResponse>(contentStream);
                return payloadResponse;
            }
        }

        public async Task<GetPayloadResponse> GetPayload(string payloadID)
        {
            using (var httpClient = new HttpClient())
            {
                var getPayloadByIDUrl = Path.Combine(this.uri, payloadID);
                var uri = new Uri(getPayloadByIDUrl);

                // add Authorization header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.accessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var httpResponse = await httpClient.GetAsync(uri);

                httpResponse.EnsureSuccessStatusCode();

                var contentStream = await httpResponse.Content.ReadAsStringAsync();

                GetPayloadResponse response = JsonConvert.DeserializeObject<GetPayloadResponse>(contentStream);
                return response;
            }
        }

        public async Task<SubscriberResponse> GetSubscribers()
        {
            using (var httpClient = new HttpClient())
            {
                var uri = new Uri(this.uri);

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

        public async Task DeleteSubscriber(string subscriberID)
        {
            var deleteSubscriberURL = Path.Combine(this.uri, subscriberID);

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Delete, deleteSubscriberURL))
                {
                    request.Headers.Add("Authorization", "Bearer " + this.accessToken);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await httpClient.SendAsync(request);

                    response.EnsureSuccessStatusCode(); // throws if not 200-299
                }
            }
        }
    }
}
