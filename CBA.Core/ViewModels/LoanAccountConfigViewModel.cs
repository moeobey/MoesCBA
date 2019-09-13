using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Core.ViewModels
{
   public class LoanAccountConfigViewModel
    {
        public IEnumerable<GlAccount> InterestIncomeGls { get; set; }
        public int Id { get; set; }
        //[Range(0.1,10)]
        public int DInterestRate { get; set; }

        public GlAccount InterestIncomeGl { get; set; }
        public int InterestIncomeGlId { get; set; }
        public bool Status { get; set; }


        public LoanAccountConfigViewModel(LoanAccountConfig config)
        {
            if (config == null)
            {
                Id = 0;
            }
            else
            {
                Id = config.Id;
                DInterestRate = config.DInterestRate;
                InterestIncomeGl = config.InterestIncomeGl;
                InterestIncomeGlId = config.InterestIncomeGlId;
                Status = config.Status;
            }


        }
    }
}
