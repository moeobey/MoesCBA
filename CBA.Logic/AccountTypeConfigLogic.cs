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
        private readonly SavingAccountConfigRepository _savingsAccContext = new SavingAccountConfigRepository(new ApplicationDbContext());
        private readonly CurrentAccountConfigRepository _currentAccContext = new CurrentAccountConfigRepository(new ApplicationDbContext());
        private readonly LoanAccountConfigRepository _loanAccContext = new LoanAccountConfigRepository(new ApplicationDbContext());

        public SavingsAccountConfig GetSavingsConfig()
        {
            var config = _savingsAccContext.GetAllSavings();
            return config;
        }
        public CurrentAccountConfig GetCurrentConfig()
        {
            var config = _currentAccContext.GetCurrentConfig();
            return config;
        }
        public LoanAccountConfig GetLoanConfig()
        {
            var config = _loanAccContext.GetLoanConfig();
            return config;
        }
        public void SaveSavings(SavingsAccountConfig config)
        {
            _savingsAccContext.Add(config);
            _savingsAccContext.Save(config);
        }
        public void SaveCurrent(CurrentAccountConfig config)
        {
            _currentAccContext.Add(config);
            _currentAccContext.Save(config);
        }
        public void SaveLoan(LoanAccountConfig config)
        {
            _loanAccContext.Add(config);
            _loanAccContext.Save(config);
        }

        public void UpdateSavings(SavingsAccountConfig savingsConfig)
        {
            _savingsAccContext.Save(savingsConfig);

        }
        public void UpdateCurrent(CurrentAccountConfig currentConfig)
        {
            _currentAccContext.Save(currentConfig);

        }
        public void UpdateLoan(LoanAccountConfig loanConfig)
        {
            _loanAccContext.Save(loanConfig);
        }
    }
}
