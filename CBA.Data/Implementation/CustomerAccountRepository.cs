using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using System.Data.Entity;

namespace CBA.Data.Implementation
{
   public class CustomerAccountRepository:Repository<CustomerAccount>
    {
        readonly ApplicationDbContext _context;

        public CustomerAccountRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
        public IEnumerable<CustomerAccount> GetAllCustomersAccounts()
        {

            return _context.CustomerAccounts.Include(c => c.Branch).OrderByDescending(c=>c.Id).ToList();
        }
        public IEnumerable<CustomerAccount> GetAllExceptLoan()
        {

            return _context.CustomerAccounts.Where(c=>c.AccountType != AccountType.Loan).ToList();
        }

        public IEnumerable<CustomerAccount> GetByAccountType(AccountType accountType)
        {

            return _context.CustomerAccounts.Where(c => c.AccountType == accountType).ToList();
        }
        

        public string GetCustomerId(int id)
        {
            var customer = _context.CustomerAccounts.FirstOrDefault(u => u.Id == id);
            return customer.CustomerId;

        }
        public CustomerAccount GetByAccountNumber(string accountNumber)
        {
            var account = _context.CustomerAccounts.FirstOrDefault(u => u.AccountNumber == accountNumber);
            return account;

        }
      
        public IEnumerable<CustomerAccount> GetOpenAccounts()
        {

            return _context.CustomerAccounts.Include(c => c.Branch).Where(c=>c.IsOpen).Where(c=>c.AccountType != AccountType.Loan).ToList();
        }
        public IEnumerable<CustomerAccount> GetLoanAccounts()
        {

            return _context.CustomerAccounts.Where(c => c.AccountType == AccountType.Loan).ToList();
        }
        



    }
}
