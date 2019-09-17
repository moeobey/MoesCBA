using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.Implementation
{
   
    public enum AccountType
    {
        Savings = 1,
        Current = 2,
        Loan = 3,

    }
    public class CustomerAccount
    {
      
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public Branch Branch { get; set; }
        public int BranchId { get; set; }
        
        public decimal Balance { get; set; }

        public AccountType AccountType { get; set; }
        public bool IsOpen { get; set; }
        public DateTime CreatedAt { get; set; }

        public decimal Lien { get; set; }
        public decimal Interest { get; set; }

        
    }
}
