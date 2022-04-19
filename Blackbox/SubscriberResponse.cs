using System;
using Newtonsoft.Json;

namespace Blackbox
{
    // SubscriberResponse myDeserializedClass = JsonConvert.DeserializeObject<SubscriberResponse>(myJsonResponse);
    public class Status
    {
        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("current")]
        public string Current { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("aggregator_name")]
        public string AggregatorName { get; set; }

        [JsonProperty("profile_name")]
        public string ProfileName { get; set; }

        [JsonProperty("last_payment")]
        public double? LastPayment { get; set; }
    }

    public class Account
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }

        [JsonProperty("customer_code")]
        public string CustomerCode { get; set; }

        [JsonProperty("aggregator_id")]
        public string AggregatorId { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("payload_id")]
        public string PayloadId { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }
    }

    public class SubscriberResponse
    {
        [JsonProperty("accounts")]
        public List<Account> Accounts { get; set; }
    }
}


