using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Data.Implementation
{
    public class BranchRepository : Repository<Branch>
    {
        readonly ApplicationDbContext _context;

        
        public BranchRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
