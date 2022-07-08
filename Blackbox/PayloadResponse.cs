using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlaggoBlackbox
{
    public class Body
    {
        [JsonProperty("body")]
        public string PayloadBody { get; set; }

        [JsonProperty("credit_comaker_id")]
        public string CreditComakerId { get; set; }

        [JsonProperty("credit_debtor_id")]
        public string CreditDebtorId { get; set; }

        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("credit_merchant_id")]
        public string CreditMerchantId { get; set; }

        [JsonProperty("credit_status")]
        public string CreditStatus { get; set; }

        [JsonProperty("receiver_id")]
        public string ReceiverId { get; set; }

        [JsonProperty("receiver_name")]
        public string ReceiverName { get; set; }

        [JsonProperty("sender_id")]
        public string SenderId { get; set; }

        [JsonProperty("sender_name")]
        public string SenderName { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("subscription_id")]
        public string SubscriptionId { get; set; }

        [JsonProperty("subscription_status")]
        public string SubscriptionStatus { get; set; }

        [JsonProperty("types")]
        public string Types { get; set; }

        [JsonProperty("aggregator_id")]
        public string AggregatorId { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("customer_code")]
        public string CustomerCode { get; set; }

        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }

        [JsonProperty("transaction_amount")]
        public int? TransactionAmount { get; set; }

        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }

        [JsonProperty("transaction_last_state_type")]
        public int? TransactionLastStateType { get; set; }

        [JsonProperty("transaction_type")]
        public int? TransactionType { get; set; }

        [JsonProperty("last_payment")]
        public string LastPayment { get; set; }
    }

    public class PayloadResponse
    {
        [JsonProperty("payloads")]
        public List<Payload> Payloads { get; set; }
    }
}
