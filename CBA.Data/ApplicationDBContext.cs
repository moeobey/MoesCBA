using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using CBA.Core;
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
        public DbSet<GlCategory> GlCategories { get; set; }
        public DbSet<GlAccount> GlAccounts { get; set; }
        public DbSet<Teller> Tellers { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }

        public DbSet<SavingsAccountConfig> SavingsAccountConfigs { get; set; }

        public DbSet<CurrentAccountConfig> CurrentAccountConfigs { get; set; }

        public DbSet<LoanAccountConfig> LoanAccountConfigs { get; set; }
        public DbSet<GlPost> GlPost { get; set; }
        public DbSet<TellerPost> TellerPosts { get; set; }

        public  DbSet<BankConfiguration> BankConfigurations { get; set; }
        public DbSet<COTPost> CotPosts { get; set; }
        

    }
}
