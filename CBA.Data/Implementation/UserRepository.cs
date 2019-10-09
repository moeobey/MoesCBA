using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using CBA.Data.Interface;

namespace CBA.Data.Implementation
{
   public class UserRepository : Repository<User>, IUserRepository
    {
        readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public User GetByUsername(string username)
        { 
            var user = _context.User.FirstOrDefault(u => u.Username == username);
            return user;

        }
        
        public User GetByEmail(string email)
        {
            var tEmail = _context.User.FirstOrDefault(u => u.Email == email);
            return tEmail;

        }


        public IEnumerable<User> GetAllWithBranch()
        {

            return _context.User.Include(c=>c.Branch).ToList();
        }
        public IEnumerable<User> GetAllUnassigned()
        {

            return _context.User.Where(u=>u.IsAssigned == false).Where(u=>u.Role=="Teller");
            
        }
        public User GetUnassigned(int id)
        {
            var user = _context.User.Where(u=>u.IsAssigned==false).FirstOrDefault(u => u.Id == id);
            return user;

        }
        public User GetCurrentUser(int id)
        {
            var user = _context.User.Include(c=>c.Branch).FirstOrDefault(u => u.Id == id);
            return user;

        }
        

    }
}
