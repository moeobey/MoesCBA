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

        public DbSet<User> User { get; set; }
        
        public DbSet<Customer> Customer { get; set; }
        public DbSet<UserRole> UserRoles { get; set; } 
        public  DbSet<Branch> Branches { get; set; }
        public DbSet<GlAccountCategory> GlAccountCategories { get; set; }
        public DbSet<GlAccount> GlAccounts { get; set; }
        public DbSet<TillAccount> TillAccounts { get; set; }
        public DbSet<Teller> Tellers { get; set; }

        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
    }
}
