using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.Implementation
{
   public class CurrentAccountConfig
    {
        public int Id { get; set; }
        public decimal CInterestRate  { get; set; }
        public decimal MinBalance { get; set; }
        public GlAccount InterestExpenseGl { get; set; }
        public int InterestExpenseGlId { get; set; }
        public GlAccount InterestPayableGl { get; set; }
        public int? InterestPayableGlId { get; set; }
        public decimal COT { get; set; }
        public  GlAccount COTIncomeGl { get;  }
        public int COTIncomeGlId { get; set; }
        public bool Status { get; set; }
    }
}
