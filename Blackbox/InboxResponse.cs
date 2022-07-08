using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlaggoBlackbox
{
    public class Sender
    {
        [JsonProperty("id")]
        public string? Id;

        [JsonProperty("name")]
        public string? Name;
    }

    public class Receiver
    {
        [JsonProperty("id")]
        public string? Id;

        [JsonProperty("name")]
        public string? Name;
    }

    public class InboxMetadata
    {
        [JsonProperty("scheduled")]
        public bool Scheduled;
    }

    public class Credit
    {
        [JsonProperty("id")]
        public string? Id;

        [JsonProperty("status")]
        public string? Status;

        [JsonProperty("debtor_id")]
        public string? DebtorId;

        [JsonProperty("comaker_id")]
        public string? ComakerId;

        [JsonProperty("merchant_id")]
        public string? MerchantId;
    }

    public class LastState
    {
        [JsonProperty("type")]
        public int Type;
    }

    public class Transaction
    {
        [JsonProperty("id")]
        public string? Id;

        [JsonProperty("type")]
        public int? Type;

        [JsonProperty("amount")]
        public int? Amount;

        [JsonProperty("last_state")]
        public LastState? LastState;
    }

    public class Message
    {
        [JsonProperty("id")]
        public string? Id;

        [JsonProperty("sender_id")]
        public string? SenderId;

        [JsonProperty("receiver_id")]
        public string? ReceiverId;

        [JsonProperty("sender")]
        public Sender? Sender;

        [JsonProperty("receiver")]
        public Receiver? Receiver;

        [JsonProperty("subject")]
        public string? Subject;

        [JsonProperty("body")]
        public string? Body;

        [JsonProperty("type")]
        public int Type;

        [JsonProperty("types")]
        public List<int>? Types;

        [JsonProperty("status")]
        public int Status;

        [JsonProperty("created_at")]
        public DateTime CreatedAt;

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt;

        [JsonProperty("metadata")]
        public InboxMetadata? Metadata;

        [JsonProperty("payload_id")]
        public string? PayloadId;

        [JsonProperty("credit")]
        public Credit? Credit;

        [JsonProperty("transaction")]
        public Transaction? Transaction;
    }

    public class InboxResponse
    {
        [JsonProperty("messages")]
        public List<Message>? Messages;

        [JsonProperty("count")]
        public int Count;
    }


}
