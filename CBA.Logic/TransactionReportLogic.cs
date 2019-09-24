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
    public class TransactionReportLogic
    {
        private readonly GlAccountRepository _glAccContext = new GlAccountRepository(new ApplicationDbContext());
        private readonly CustomerAccountLogic _customerAccContext = new CustomerAccountLogic();
        public List<GlAccount> GetAllAssetAccounts()
        {
            var assetAccounts = _glAccContext.GetByMainCategory(MainAccountCategory.Asset);
            var loanAccountsBalance = _customerAccContext.GetLoanAccounts().Sum(c=>c.Balance);
            var loanAssetAcc = new GlAccount
            {
                Name = "Loan Accounts",
                Balance = loanAccountsBalance
            };
            assetAccounts.Add(loanAssetAcc);
            return assetAccounts;

        }
        public List<GlAccount> GetAllLiabilityAccounts()
        {
            var liabilityAccounts = _glAccContext.GetByMainCategory(MainAccountCategory.Liability);
            var savingsAccBalance = _customerAccContext.GetByAccountType(AccountType.Savings).Sum(c => c.Balance);
            var currentAccBalance = _customerAccContext.GetByAccountType(AccountType.Current).Sum(c => c.Balance);

            var customerSavingsLiabilityAcc = new GlAccount
            {
                Name = "Customers Savings Liability Accounts",
                Balance = savingsAccBalance
            };
            var customerCurrentLiabilityAcc = new GlAccount
            {
                Name = "Customers  Current Liability Accounts",
                Balance = currentAccBalance
            };
            
            liabilityAccounts.Add(customerSavingsLiabilityAcc);
            liabilityAccounts.Add(customerCurrentLiabilityAcc);
            return liabilityAccounts;
        }
        public List<GlAccount> GetAllCapitalAccounts()
        {
            var capitalAccounts = _glAccContext.GetByMainCategory(MainAccountCategory.Capital);
            var incomeTotal = _glAccContext.GetByMainCategory(MainAccountCategory.Income).Sum(c=>c.Balance);
            var expenseTotal = _glAccContext.GetByMainCategory(MainAccountCategory.Expense).Sum(c=>c.Balance);
            var otherCapitalAcc = new GlAccount
                {
                    Name = "Reserve Capital Accounts",
                    Balance = incomeTotal-expenseTotal
                };
            capitalAccounts.Add(otherCapitalAcc);
            return capitalAccounts;
        }

        public List<GlAccount> GetAllIncomeAccounts()
        {
            var interestAccounts = _glAccContext.GetByMainCategory(MainAccountCategory.Income);

            return interestAccounts;
        }
        public List<GlAccount> GetAllExpenseAccounts()
        {
            var interestAccounts = _glAccContext.GetByMainCategory(MainAccountCategory.Expense);

            return interestAccounts;
        }
        


    }
}
