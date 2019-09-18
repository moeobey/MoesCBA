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

            return _context.GlAccounts.Include(c=>c.Branch).Include(c=>c.GlCategory).ToList();
        }

        public IEnumerable<GlAccount> GetAllUnassigned()
        {
        
            return _context.GlAccounts.Where(u => u.IsAssigned == false).Where(u=>u.IsTillAccount == true);

        }
        public GlAccount GetByName(string name)
        {
            var tName = _context.GlAccounts.FirstOrDefault(u => u.Name.ToLower() == name.ToLower());
            return tName;

        }
        public GlAccount GetByAccCode(long accCode)
        {
            var account = _context.GlAccounts.FirstOrDefault(u => u.AccountCode == accCode);
            return account;

        }
        

        public IEnumerable<GlAccount> GetAllExpenseAccount()
        {

            return _context.GlAccounts.Where(u => u.AccountCode.ToString().StartsWith("5")).Where(u=>u.IsAssigned == false);
        }
        public IEnumerable<GlAccount> GetAllIncomeAccount()
        {

            return _context.GlAccounts.Where(u => u.AccountCode.ToString().StartsWith("4")).Where(u => u.IsAssigned == false);
        }

        public long GetAccCode(int id)
        {
            var result = _context.GlAccounts.FirstOrDefault(u=>u.Id == id);
            var cResult = Convert.ToInt64(result.AccountCode);
            return cResult;
        }
        public long GetVaultAccCode()
        {
            var result = _context.GlAccounts
                .Where(c=>c.Name.ToLower().Contains("vault"))
                .FirstOrDefault(a => a.GlCategory.Name.ToLower() =="cash asset");
            long cResult = 0;
            cResult = result != null ? Convert.ToInt64(result.AccountCode) : 0;
            return cResult;
        }
        

    }
}
