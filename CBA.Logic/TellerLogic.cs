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
   public class TellerLogic
    {
        private readonly TellerRepository _db = new TellerRepository(new ApplicationDbContext());
        public void Save(Teller teller)
        {
            _db.Add(teller);
            _db.Save(teller);
        }
        public IEnumerable<Teller> GetWithAll()
        {
            var values = _db.GetWithAll();
            return values;
        }
        public long GetTillAccount(int userId)
        {
            var result = _db.GetTillAccount(userId);
            return result;
        }

        



    }
}
