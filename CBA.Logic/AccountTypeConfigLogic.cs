using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using CBA.Data;
using CBA.Data.Implementation;

namespace CBA.Logic
{
   public class AccountTypeConfigLogic
    {
        private readonly SavingAccountConfigRepository _savingsContext = new SavingAccountConfigRepository(new ApplicationDbContext());
        private readonly CurrentAccountConfigRepository _currentContext = new CurrentAccountConfigRepository(new ApplicationDbContext());
        private readonly LoanAccountConfigRepository _loanContext = new LoanAccountConfigRepository(new ApplicationDbContext());

        public SavingsAccountConfig GetSavingsConfig()
        {
            var config = _savingsContext.GetAllSavings();
            return config;
        }
        public CurrentAccountConfig GetCurrentConfig()
        {
            var config = _currentContext.GetCurrentConfig();
            return config;
        }
        public void SaveSavings(SavingsAccountConfig config)
        {
            _savingsContext.Add(config);
            _savingsContext.Save(config);
        }
        
        public void UpdateSavings(SavingsAccountConfig savingsConfig)
        {
            _savingsContext.Save(savingsConfig);

        }
    }
}
