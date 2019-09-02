using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Data
{
    public class ApplicationDbContext:DbContext 
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        private DbSet<User> User { get; set; }
    }
}
