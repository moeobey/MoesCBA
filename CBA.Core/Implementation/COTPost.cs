using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Core.Implementation
{
   public class COTPost
    {
        public int Id { get; set; }
        public CustomerAccount AccountToDebit { get; set; }
        public int? AccountToDebitId { get; set; }
        public GlAccount AccountToCredit { get; set; }
        public int? AccountToCreditId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PostedAt { get; set; }
    }
}
