using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Data.Implementation
{
  public  class GlAccountRepository:Repository<GlAccount>
    {
        readonly ApplicationDbContext _context;

        public GlAccountRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
          
        }
        public IEnumerable<GlAccount> GetAllGlAccountsWithBranch()
        {

            return _context.GlAccounts.Include(c=>c.Branch).Include(c=>c.GlAccountCategory).ToList();
        }
        public IEnumerable<GlAccount> GetAllUnassigned()
        {
        
            return _context.GlAccounts.Where(u => u.IsAssigned == false);

        }
    }
}
