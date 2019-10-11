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
   public class BankConfigLogic
    {
        private readonly BankConfigRepository _db = new BankConfigRepository(new ApplicationDbContext());

        public void Save(BankConfiguration config)
        {
            _db.Add(config);
            _db.Save(config);
        }
        public BankConfiguration GetConfig()
        {
            var config = _db.GetConfig();
            return config;
        }
        public void Update(BankConfiguration config)
        {
            _db.Save(config);
        }
        public void IncreaseFinancialDate()
        {
            var config = GetConfig();
            config.FinancialDate = config.FinancialDate.AddDays(1);
            _db.Save(config);
        }
    }
}
