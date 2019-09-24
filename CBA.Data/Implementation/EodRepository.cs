using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Data.Implementation
{
    public class EodRepository : Repository<TransactionLog>
    {
        public EodRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
