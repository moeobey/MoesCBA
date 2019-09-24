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
        public IEnumerable<GlAccount> GetAllLiabilityAccount()
        {

            return _context.GlAccounts.Where(u => u.AccountCode.ToString().StartsWith("2")).Where(u => u.IsAssigned == false);
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
        public GlAccount GetVault()
        {
            //var valutCode = System.Configuration.ConfigurationManager.AppSettings["Vault"];
            var result = _context.GlAccounts
                .Where(c=>c.Name.ToLower().Contains("vault"))
                .FirstOrDefault(a => a.GlCategory.Name.ToLower() =="cash asset");
            
            return result;
        }
        public IEnumerable<GlAccount> GetAllAssetAccounts()
        {
            var assetAccount =  _context.GlAccounts.Where(u => u.GlCategory.MainAccountCategory == MainAccountCategory.Asset).ToList();
        
            return assetAccount;
        }
        public List<GlAccount> GetByMainCategory(MainAccountCategory mainCategory)
        {
            var accounts = _context.GlAccounts.Where(u => u.GlCategory.MainAccountCategory == mainCategory).ToList();

            return accounts;
        }
        

    }
}
