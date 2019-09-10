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

            return _context.CustomerAccounts.Include(c => c.Branch).ToList();
        }
     
    }
}
