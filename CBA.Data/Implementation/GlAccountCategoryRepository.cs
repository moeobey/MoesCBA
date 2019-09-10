using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Data.Implementation
{
    public class GlAccountCategoryRepository: Repository<GlAccountCategory>
    {
        readonly ApplicationDbContext _context;
        public GlAccountCategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        
        public string GetMainAccountType(int id)
        {
            var mainCategory = _context.GlAccountCategories.FirstOrDefault(u => u.Id == id);
            var mainCategoryId = "";
            if (mainCategory != null)
            {
                 mainCategoryId = mainCategory.MainAccountCategory.ToString();
            }

            return mainCategoryId;

        }
    }
}
