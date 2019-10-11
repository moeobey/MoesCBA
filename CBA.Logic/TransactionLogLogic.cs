using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using CBA.Data;
using CBA.Data.Implementation;

namespace CBA.Logic
{
    public class TransactionLogLogic
    {
        private readonly TransactionLogRepository _db = new TransactionLogRepository(new ApplicationDbContext());

        public void LogTransaction(GlAccount glAccount, decimal amount,TransactionType transactionType)//overload for gl accounts
        {
            var transaction = new TransactionLog()
            {
                Name = glAccount.Name,
                Amount = amount,
                Date = DateTime.Now,
                TransactionType = transactionType,
                AccountCode = glAccount.AccountCode,
                MainAccountCategory =GetMainCategory(glAccount),

            };
            _db.Add(transaction);
            _db.Save(transaction);
        }
        public MainAccountCategory GetMainCategory(GlAccount glAccount)
        {
            var gl = glAccount.AccountCode.ToString();

            if (gl.StartsWith("1"))
            {
                return MainAccountCategory.Asset;
            }
            else if(gl.StartsWith("2"))
            {
                return MainAccountCategory.Liability;
            }
            else if(gl.StartsWith("3"))
            {
                return MainAccountCategory.Capital;
            }
            else if (gl.StartsWith("4"))
            {
                return MainAccountCategory.Income;
            }
            else
            {
                return MainAccountCategory.Expense;
            }
        }
        public void LogTransaction(CustomerAccount customerAccount, decimal amount, TransactionType transactionType)//overload for customers accounts
        {
            var transaction = new TransactionLog
            {
                Name = customerAccount.AccountName,
                Amount = amount,
                Date = DateTime.Now,
                AccountCode = Convert.ToInt64(customerAccount.AccountNumber),
                TransactionType = transactionType,
                MainAccountCategory = customerAccount.AccountType == AccountType.Loan //Exception: Loan accounts are assets
                    ? MainAccountCategory.Asset
                    : MainAccountCategory.Liability,
            };
            _db.Add(transaction);
            _db.Save(transaction);
        }
        public IEnumerable<TransactionLog> GetAllLogs()
        {
            var values = _db.GetAllLogs();
            return values;
        }
        public IEnumerable<TransactionLog> GetLogsByPeriod(string startDate, string endDate)
        {
            var values = _db.GetLogsByPeriod(startDate, endDate).ToList();
            return values;
        }
    }
}
