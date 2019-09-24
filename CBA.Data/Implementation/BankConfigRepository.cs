using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Data.Implementation
{
   public class BankConfigRepository: Repository<BankConfiguration>
    {
        readonly ApplicationDbContext _context;

        public BankConfigRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public BankConfiguration GetConfig()
        {
            return _context.BankConfigurations.FirstOrDefault();

        }

      
    }
}
