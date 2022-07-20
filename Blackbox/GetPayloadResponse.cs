using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Blaggo.Blackbox
{

    public class GetPayloadBody
    {
        [JsonProperty("body")]
        public string ResponseBody;

        [JsonProperty("receiver_id")]
        public string ReceiverId;

        [JsonProperty("sender_id")]
        public string SenderId;

        [JsonProperty("subject")]
        public string Subject;

        [JsonProperty("transaction_amount")]
        public int TransactionAmount;

        [JsonProperty("transaction_id")]
        public string TransactionId;

        [JsonProperty("transaction_last_state_type")]
        public int TransactionLastStateType;

        [JsonProperty("transaction_type")]
        public int TransactionType;

        [JsonProperty("types")]
        public string Types;

        [JsonProperty("user_id")]
        public string UserId;
    }

    public class Payload
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("status")]
        public int Status;

        [JsonProperty("body")]
        public Body Body;

        [JsonProperty("created_at")]
        public DateTime CreatedAt;
    }

    public class GetPayloadResponse
    {
        [JsonProperty("payload")]
        public Payload Payload;
    }
}
