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
  public   class GlAccountLogic
    {
        private readonly GlAccountRepository _db = new GlAccountRepository(new ApplicationDbContext());
        private readonly GlAccountCategoryRepository _category = new GlAccountCategoryRepository(new ApplicationDbContext());

        public IEnumerable<GlAccount> GetAllGlAccounts()
        {
            var value = _db.GetAllGlAccountsWithBranch();
            return value;
        }
        public IEnumerable<GlAccountCategory> GetGlAccountCategories()
        {
            return _category.GetAll();
        }
        public void Save(GlAccount account)
        {

            _db.Add(account);
            _db.Save(account);
        }
        public GlAccount Get(int id)
        {

            var values = _db.Get(id);
            return values;
        }
        public void Update(GlAccount account)
        {
            _db.Save(account);
        }
        public IEnumerable<GlAccount> GetAll()
        {
            var values = _db.GetAll();
            return values;
        }


        public string GenerateGlAccountCode(int categoryId)
        {
            var ids = _db.GetAll().OrderByDescending(b => b.Id);
            var accountId = 1;
            if (ids.Count() > 1)
            {
                accountId = ids.First().Id;
                accountId++;
            }
                
            //var value = accountId + 1;
           
            var mainAccountType = _category.GetMainAccountType(categoryId);
            switch (mainAccountType)
            {
                case "Asset":
                    return "10" + accountId;
                case "Liability":
                    return "20" + accountId;
                case "Capital":
                    return "30" + accountId;
                case "Income":
                    return "40" + accountId;
                case "Expense":
                    return "50" + accountId;
                default:
                    return "";
            }



        }
        public IEnumerable<GlAccount> GetAllUnassigned()
        {
            var values = _db.GetAllUnassigned();
            return values;
        }
    }
}
