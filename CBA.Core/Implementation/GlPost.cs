using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.Implementation
{
   public class GlPost
    {
        public int Id { get; set; }
        public GlAccount GlAccountToCredit { get; set; }
        public int? GlAccountToCreditId { get; set; }
        public GlAccount GlAccountToDebit { get; set; }
        public int? GlAccountToDebitId { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public  User User { get; set; }
        public int UserId { get; set; }
        public DateTime PostedAt { get; set; }
    }
}
