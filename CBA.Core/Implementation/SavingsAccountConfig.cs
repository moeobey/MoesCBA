using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.Implementation
{
   public class SavingsAccountConfig
    {
        public int Id { get; set; }         
        //[Range(0.1,10)]
        public int CInterestRate  { get; set; }
        public decimal MinBalance { get; set; }

        public int InterestExpenseGlId { get; set; }
        public bool Status { get; set; }

    }
}
