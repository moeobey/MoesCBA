using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Core.ViewModels
{
  public  class CurrentAccountConfigViewModel
    {
        public int Id { get; set; }
        public decimal CInterestRate { get; set; }
        public IEnumerable<GlAccount> InterestExpenseGls { get; set; } 
        public IEnumerable<GlAccount> InterestPayableGls { get; set; } 
        public IEnumerable<GlAccount> COTIncomeGls { get; set; }
        public GlAccount InterestExpenseGl { get; set; }
        public int InterestExpenseGlId { get; set; }
        public decimal MinBalance { get; set; }
        public GlAccount InterestPayableGl { get; set; }
        public int InterestPayableGlId { get; set; }
        public decimal COT { get; set; }
        public GlAccount COTIncomeGl { get; set; }
        public int COTIncomeGlId { get; set; }
        public bool Status { get; set; }

        public CurrentAccountConfigViewModel(CurrentAccountConfig config)
        {
            if (config == null)
            {
                Id = 0;
            }
            else
            {
                Id = config.Id;
                CInterestRate = config.CInterestRate;
                InterestExpenseGl = config.InterestExpenseGl;
                InterestExpenseGlId = config.InterestExpenseGlId;
                COTIncomeGl = config.COTIncomeGl;
                COTIncomeGlId = config.COTIncomeGlId;
                InterestPayableGlId = (int) config.InterestPayableGlId;
                COT = config.COT;
                MinBalance = config.MinBalance;
                Status = config.Status;
            }
        }
    }
}
