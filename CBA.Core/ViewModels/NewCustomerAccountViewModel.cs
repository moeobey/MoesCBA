using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Core.ViewModels
{
  public   class NewCustomerAccountViewModel
    {
        public IEnumerable<Branch> Branches { get; set; }
        public Customer Customer { get; set; }
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public int BranchId { get; set; }
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; }
        public bool IsOpen { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Interest { get; set; }
        public decimal Lien { get; set; }
        public NewCustomerAccountViewModel()
        {
            Id = 0;
        }

        public NewCustomerAccountViewModel(CustomerAccount account)
        {
            Id = account.Id;
            CustomerId = account.CustomerId;
            AccountName = account.AccountName;
            BranchId = account.BranchId;
            Balance = account.Balance;
            AccountType = account.AccountType;
            IsOpen = account.IsOpen;
            CreatedAt = account.CreatedAt;
            Interest = account.Interest;
            Lien = account.Lien;
        }
    }
}
