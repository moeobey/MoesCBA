using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using System.Data.Entity;

namespace CBA.Data.Implementation
{
  public  class CurrentAccountConfigRepository:Repository<CurrentAccountConfig>
  {
      private readonly ApplicationDbContext _context;
        public CurrentAccountConfigRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        
        public CurrentAccountConfig GetCurrentConfig()
        {
            return _context.CurrentAccountConfigs.Include(c=>c.InterestExpenseGl).Include(c=>c.InterestPayableGl).FirstOrDefault();

        }
    }
}
