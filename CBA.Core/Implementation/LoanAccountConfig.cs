using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.Implementation
{
   public class LoanAccountConfig
    {
        public int Id { get; set; }
        public decimal DInterestRate  { get; set; }
        public GlAccount InterestIncomeGl { get; set; }
        public int InterestIncomeGlId { get; set; }
        public bool Status { get; set; }
    }
}
