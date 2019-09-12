using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Data.Implementation
{
   public class LoanAccountConfigRepository:Repository<LoanAccountConfig>
    {
        private readonly ApplicationDbContext _context;
        public LoanAccountConfigRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        
        public LoanAccountConfig GetLoanConfig()
        {
            return _context.LoanAccountConfigs.FirstOrDefault();

        }
    }
}
