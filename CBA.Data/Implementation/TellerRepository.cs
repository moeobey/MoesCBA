using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using System.Data.Entity;

namespace CBA.Data.Implementation
{
    public class TellerRepository: Repository<Teller>
    {
        readonly ApplicationDbContext _context;

        public TellerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Teller> GetWithAll()
        {

            return _context.Tellers
                .Include(c=>c.User)
                .Include(c=>c.GlAccount).ToList();
        }
        public long GetTillAccount(int userId)
        {
            var result = _context.Tellers.Include(c=>c.GlAccount).FirstOrDefault(u => u.UserId == userId);
            if (result != null)
            {
                return result.GlAccount.AccountCode;

            }
            else
            {
                return 0;
            }

        }

        

    }
}
