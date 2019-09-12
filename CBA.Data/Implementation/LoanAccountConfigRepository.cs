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
        public LoanAccountConfigRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
