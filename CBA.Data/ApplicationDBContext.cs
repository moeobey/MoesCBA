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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<Decimal>().Configure(config => config.HasPrecision(20, 10));
            modelBuilder.Entity<SavingsAccountConfig>().Property(c=>c.CInterestRate).HasPrecision(20, 2);
            modelBuilder.Entity<SavingsAccountConfig>().Property(c=>c.MinBalance).HasPrecision(20, 2);
            modelBuilder.Entity<CurrentAccountConfig>().Property(c=>c.MinBalance).HasPrecision(20, 2);
            modelBuilder.Entity<CurrentAccountConfig>().Property(c=>c.MinBalance).HasPrecision(20, 2);
            modelBuilder.Entity<LoanAccountConfig>().Property(c=>c.DInterestRate).HasPrecision(20, 2);
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

        public DbSet<Loan> Loans { get; set; }
        public DbSet<TransactionLog> TransactionLogs { get; set; }

        

    }
}
