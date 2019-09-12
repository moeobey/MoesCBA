using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Core.ViewModels
{
   public class SavingsAccountConfigViewModel
    {
        
        public int Id   { get; set; }
        public decimal CInterestRate { get; set; }
        public IEnumerable<GlAccount> GlAccounts { get; set; }

        
        public GlAccount GlAccount { get; set; }
        public int GlAccountId { get; set; }
        public decimal MinBalance { get; set; }

      
        public bool Status { get; set; }
      

    
        public SavingsAccountConfigViewModel( SavingsAccountConfig config)
        {
            if (config == null)
            {
                Id = 0;
            }
            else
            {
                Id = config.Id;
                CInterestRate = config.CInterestRate;
                GlAccountId = config.GlAccountId;   
                MinBalance = config.MinBalance;
                Status = config.Status;
                GlAccount = config.GlAccount;
            }
          

        }

    }

}
