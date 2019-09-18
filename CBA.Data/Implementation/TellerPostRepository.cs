using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using  System.Data.Entity;

namespace CBA.Data.Implementation
{
    public class TellerPostRepository:Repository<TellerPost>
    {
        private readonly ApplicationDbContext _context;
        public TellerPostRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<TellerPost> GetAllPosts(int tellerId)
        {

            return _context.TellerPosts.Include(c => c.CustomerAccount).Where(c=>c.TellerId == tellerId).ToList();
        }


    }
}
