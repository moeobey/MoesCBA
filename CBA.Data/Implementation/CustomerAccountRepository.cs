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


    }
}
