using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using CBA.Data;
using CBA.Data.Implementation;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web.Helpers;


namespace CBA.Logic
{
    public class AccountLogic:Controller
    {
        private readonly UserRepository _db = new UserRepository(new ApplicationDbContext());

        public User GetByUsername(string username)
        {
            var user =_db.GetByUsername(username);
            return user;
        }   

        public bool Authenticate(User user)
        {
            bool isAUser = false;
            var getUser = _db.GetByUsername(user.Username);
            if (getUser != null)
            {
                var pass = Crypto.Hash(user.Password.Trim());
                if (String.CompareOrdinal(getUser.Password, pass) == 0)
                {
                    isAUser = true;
                }
            }
            return isAUser;
        }
    }
}
