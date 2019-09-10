using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Data.Implementation
{
    public class RoleRepository : Repository<UserRole>
    {
        readonly ApplicationDbContext _context;


        public RoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
