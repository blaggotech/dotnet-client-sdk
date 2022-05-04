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
        private static readonly string? BLACKBOX_BASE_URL = Environment.GetEnvironmentVariable("BLACKBOX_BASE_URL");

        public Blackbox(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public async Task<PayloadResponse?> GetPayloads(HttpClient httpClient)
        {
            var payloadsUrl = BLACKBOX_BASE_URL + "/payloads";
            var uri = new Uri(payloadsUrl);

            // add Authorization header
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.accessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponse = await httpClient.GetAsync(uri);

            httpResponse.EnsureSuccessStatusCode();

            var contentStream = await httpResponse.Content.ReadAsStringAsync();

            PayloadResponse? payloadResponse = JsonConvert.DeserializeObject<PayloadResponse>(contentStream);
            return payloadResponse;
        }

        public async Task<GetPayloadResponse?> GetPayload(HttpClient httpClient, string payloadID)
        {
            var payloadsUrl = BLACKBOX_BASE_URL + "/payloads";
            var getPayloadByIDUrl = Path.Combine(payloadsUrl, payloadID);
            var uri = new Uri(getPayloadByIDUrl);

            // add Authorization header
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.accessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponse = await httpClient.GetAsync(uri);

            httpResponse.EnsureSuccessStatusCode();

            var contentStream = await httpResponse.Content.ReadAsStringAsync();

            GetPayloadResponse? response = JsonConvert.DeserializeObject<GetPayloadResponse>(contentStream);
            return response;
        }

        public async Task<SubscriberResponse?> GetSubscribers(HttpClient httpClient)
        {
            var subscribersUrl = BLACKBOX_BASE_URL + "/accounts";
            var uri = new Uri(subscribersUrl);

            // add Authorization header
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.accessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponse = await httpClient.GetAsync(uri);

            httpResponse.EnsureSuccessStatusCode();

            var contentStream = await httpResponse.Content.ReadAsStringAsync();

            SubscriberResponse? subscriberResponse = JsonConvert.DeserializeObject<SubscriberResponse>(contentStream);
            return subscriberResponse;
        }

        public async Task DeleteSubscriber(HttpClient httpClient, string subscriberID)
        {
            var subscribersUrl = BLACKBOX_BASE_URL + "/accounts";
            var deleteSubscriberURL = Path.Combine(subscribersUrl, subscriberID);

            using (var request = new HttpRequestMessage(HttpMethod.Delete, deleteSubscriberURL))
            {
                request.Headers.Add("Authorization", "Bearer " + this.accessToken);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode(); // throws if not 200-299
            }
        }

        public async Task DeleteProtocolPayload(HttpClient httpClient, string payloadId)
        {
            var subscribersUrl = BLACKBOX_BASE_URL + "/payloads?id=" + payloadId;
           
            using (var request = new HttpRequestMessage(HttpMethod.Delete, subscribersUrl))
            {
                request.Headers.Add("Authorization", "Bearer " + this.accessToken);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode(); // throws if not 200-299
            }
        }
    }
}
