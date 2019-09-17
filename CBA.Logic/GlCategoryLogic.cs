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
   public class GlCategoryLogic
    {
        private readonly GlCategoryRepository _db = new GlCategoryRepository(new ApplicationDbContext());
     
        public void Save(GlCategory category)
        {
    
            _db.Add(category);
            _db.Save(category);
        }
        public GlCategory Get(int id)
        {

            var values = _db.Get(id);
            return values;
        }
        public void Update(GlCategory category)
        {
            _db.Save(category);
        }
        public IEnumerable<GlCategory> GetAll()
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
