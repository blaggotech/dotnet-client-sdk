using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaggoBlackbox
{
    public class AddProtocolPayloadParameters
    {
        public string? ProfileId { get; set; }
        public string? AggregatorId { get; set; }

        public string? CustomerCode { get; set; }
        public int? LastAmount { get; set; }
        public int? Status { get; set; }
        public string? Alias { get; set; }
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }

        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? Type { get; set; }
    }
}
