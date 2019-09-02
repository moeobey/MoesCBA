using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using CBA.Data;
using CBA.Data.Implementation;
using CBA.Data.Interface;

namespace CBA.Logic
{
 public  class UserLogic
    {
        private readonly IUserRepository _db = new UserRepository(new ApplicationDbContext());
        public void Save(User user)
        {
            //Console.WriteLine("trying to add user");
            _db.Add(user);
            _db.Save(user);
        }
        public User Get(int id)
        {

            var values = _db.Get(id);
            return values;
        }

        public void Update(User user)
        {
            _db.Save(user);
        }

        public IEnumerable<User> GetAll()
        {
            var values = _db.GetAll();
            return values;
        }

        public void Remove(User user)
        {
            _db.Remove(user);
            _db.Save(user);
        }

    }
}
