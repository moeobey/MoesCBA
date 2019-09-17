using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Data.Implementation
{
    public class GlCategoryRepository: Repository<GlCategory>
    {
        readonly ApplicationDbContext _context;
        public GlCategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        
        public string GetMainAccountType(int id)
        {
            var mainCategory = _context.GlCategories.FirstOrDefault(u => u.Id == id);
            var mainCategoryId = "";
            if (mainCategory != null)
            {
                 mainCategoryId = mainCategory.MainAccountCategory.ToString();
            }

            return mainCategoryId;

        }
    }
}
