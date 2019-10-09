using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using System.Data.Entity;

namespace CBA.Data.Implementation
{
   public class GlPostingRepository: Repository<GlPost>
   {
       private readonly ApplicationDbContext _context;
        public GlPostingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<GlPost> GetAllPosts()
        {
         
            return _context.GlPost
                .Include(p => p.User)
                .Include(p => p.GlAccountToCredit)
                .Include(p => p.GlAccountToDebit).ToList(); ;
        }
        public  GlPost GetAllPosts(int id)
        {
            return _context.GlPost.Include(c => c.GlAccountToCredit).Include(c => c.GlAccountToDebit).Include(c=>c.User)
                .FirstOrDefault(c => c.Id == id);
        }


    }
}
