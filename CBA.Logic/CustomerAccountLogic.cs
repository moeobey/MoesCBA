using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using CBA.Data;
using CBA.Data.Implementation;

namespace CBA.Logic
{
    public class CustomerAccountLogic
    {
        private readonly CustomerAccountRepository _db = new CustomerAccountRepository(new ApplicationDbContext());
        private readonly LoanRepository _loanDb = new LoanRepository(new ApplicationDbContext());
        private readonly TransactionLogLogic _logContext = new TransactionLogLogic();

        public void Save(CustomerAccount customer)
        {
            //Console.WriteLine("trying to add user");
            _db.Add(customer);
            _db.Save(customer);
        }
        public CustomerAccount Get(int id)
        {

            var values = _db.Get(id);
            return values;
        }
     
        public IEnumerable<CustomerAccount> GetOpenAccounts()
        {
            var value = _db.GetOpenAccounts();
            return value;
        }
        public IEnumerable<CustomerAccount> GetLoanAccounts()
        {
            var value = _db.GetLoanAccounts();
            return value;
        }
        

        public IEnumerable<CustomerAccount> GetAllExceptLoan()
        {
            var value = _db.GetAllExceptLoan();
            return value;
        }
        public IEnumerable<CustomerAccount> GetByAccountType(AccountType accountType)
        {
            var value = _db.GetByAccountType(accountType);
            return value;
        }
        
        public IEnumerable<CustomerAccount> GetAllCustomersAccounts()
        {
            var value = _db.GetAllCustomersAccounts();
            return value;
        }
        public string GetCustomerId(int id)
        {
            var values = _db.GetCustomerId(id);
            return values;
        }
        public CustomerAccount GetByAccountNumber(string accountNumber)
        {
            var values = _db.GetByAccountNumber(accountNumber);
            return values;
        }
        public void Update(CustomerAccount account)
        {
            _db.Save(account);
        }
        public string GenerateAccountNumber(string customerId, string accountType)
        {
            var random = new Random();
            var value = random.Next(0, 999).ToString("D3");

            switch (accountType)
            {
                case "Savings":
                    return "1" +value+ customerId;
                case "Current":
                    return "2" + value + customerId;
                case "Loan":
                    return "3" + value + customerId;
                default:
                    return "";
            }

        }
        public void CreditCustomer(CustomerAccount customerAccount, decimal amount)
        {
            var account = new CustomerAccount();
            if (customerAccount.AccountType == AccountType.Loan)
            {
                customerAccount.Balance -= amount;
            }
            else
            {
                customerAccount.Balance += amount;
            }
            _logContext.LogTransaction(customerAccount, amount, TransactionType.Credit);
            _db.Save(account);
        }
        public void DebitCustomer(CustomerAccount customerAccount, decimal amount)
        {
            var account = new CustomerAccount();
            if (customerAccount.AccountType == AccountType.Loan)
            {
                customerAccount.Balance += amount;
            }
            else
            {
                customerAccount.Balance -= amount;
            }
            _logContext.LogTransaction(customerAccount, amount, TransactionType.Debit);

            _db.Save(account);
        }
      
        

    }
}
