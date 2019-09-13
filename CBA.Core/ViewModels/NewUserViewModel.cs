using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Core.ViewModels
{
    public class NewUserViewModel
    {
    
        public IEnumerable<Branch> Branches { get; set; }
        public User User { get; set; }
        public int Id { get; set; }



        public IEnumerable<UserRole> UserRoles { get; set; }

    }
 
}
