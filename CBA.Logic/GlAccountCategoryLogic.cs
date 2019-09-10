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
   public class GlAccountCategoryLogic
    {
        private readonly GlAccountCategoryRepository _db = new GlAccountCategoryRepository(new ApplicationDbContext());
     
        public void Save(GlAccountCategory category)
        {
    
            _db.Add(category);
            _db.Save(category);
        }
        public GlAccountCategory Get(int id)
        {

            var values = _db.Get(id);
            return values;
        }
        public void Update(GlAccountCategory category)
        {
            _db.Save(category);
        }
        public IEnumerable<GlAccountCategory> GetAll()
        {
            var values = _db.GetAll();
            return values;
        }

       public string GetMainAccountType(int categoryId)
       {
           var value = _db.GetMainAccountType(categoryId);
           return value;
       }

    }
}
