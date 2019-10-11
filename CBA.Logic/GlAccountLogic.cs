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
        private readonly GlCategoryRepository _category = new GlCategoryRepository(new ApplicationDbContext());

        public IEnumerable<GlAccount> GetAllGlAccounts()
        {
            var value = _db.GetAllGlAccountsWithBranch();
            return value;
        }
        public IEnumerable<GlCategory> GetGlCategories()
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
        public long GetAccCode(int id)
        {
            var value = _db.GetAccCode(id);
            return value;
        }
        public GlAccount GetVault()
        {
            var value = _db.GetVault();
            return value;
        }
        public GlAccount GetByAccCode(long accCode)
        {
            var values = _db.GetByAccCode(accCode);
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
        public IEnumerable<GlAccount> GetAllExpenseAccount()
        {
            var values = _db.GetAllExpenseAccount();
            return values;
        }
        public IEnumerable<GlAccount> GetAllLiabilityAccount()
        {
            var values = _db.GetAllLiabilityAccount();
            return values;
        }
        
        public IEnumerable<GlAccount> GetAllIncomeAccount()
        {
            var values = _db.GetAllIncomeAccount();
            return values;
        }
        public long GenerateGlAccountCode(int categoryId)
        {
            var ids = _db.GetAll().OrderByDescending(b => b.Id);
            var accountId = "000000001";
            if (ids.Any())//check if table has data else use default account ID
            {
                var lastCustomerId = ids.First().Id;
                lastCustomerId++;
                accountId = lastCustomerId.ToString().PadLeft(9, '0');
            }
            var mainAccountType = _category.GetMainAccountType(categoryId);
            switch (mainAccountType)
            {
                case "Asset":
                    var assetValue = "1" + accountId;
                    return Convert.ToInt64(assetValue);
                case "Liability":
                    var liabilityValue = "2" + accountId;
                    return Convert.ToInt64(liabilityValue);
                case "Capital":
                    var capitalValue = "3" + accountId;
                    return Convert.ToInt64(capitalValue);
                case "Income":
                    var incomeValue = "4" + accountId;
                    return Convert.ToInt64(incomeValue);
                case "Expense":
                    var expenseValue = "5" + accountId;
                    return Convert.ToInt64(expenseValue);
                default:
                    return 0;
            }
        }
        public IEnumerable<GlAccount> GetAllUnassigned()
        {
            var values = _db.GetAllUnassigned();
            return values;
        }
        public GlAccount GetGlAccount(int id)
        {
            var values = _db.GetGlAccount(id);
            return values;
        }
        public bool NameIsUnique(string name)
        {
            bool isUnique = false;
            var tName = _db.GetByName(name);
            if (tName == null)
                isUnique = true;

            return isUnique;
        }
    }
}
