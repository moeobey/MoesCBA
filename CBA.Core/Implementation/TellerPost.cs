using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.Implementation
{

    public enum PostType
    {
        Deposit = 1,
        Withdrawal = 2  
    }
    public class TellerPost
    {
        public int Id { get; set; }
        public CustomerAccount CustomerAccount { get; set; }
        public int CustomerAccountId { get; set; }

        public PostType PostType { get; set; }
        public decimal Amount { get; set; }
        public string  Narration { get; set; }

        public  int TellerId { get; set; }
        public DateTime PostedAt { get; set; }


    }
}
