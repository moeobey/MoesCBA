using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Core.ViewModels
{
   public class NewGlAccountViewModel
    {
        public IEnumerable<Branch> Branches { get; set; }
        public IEnumerable<GlAccountCategory> GlAccountCategories { get; set; }

        public int? Id { get; set; }
        public string Name { get; set; }

        public int? GlAccountCategoryId { get; set; }
        public int? BranchId { get; set; }
        public int AccountCode { get; set; }
        public bool IsAssigned { get; set; }
        public SqlMoney Balance { get; set; }
        public NewGlAccountViewModel()
        {
            Id = 0;
        }

        public NewGlAccountViewModel(GlAccount glAccount)
        {
            Id = glAccount.Id;
            Name = glAccount.Name;
            GlAccountCategoryId = glAccount.GlAccountCategoryId;
            BranchId = glAccount.BranchId;
            AccountCode = glAccount.AccountCode;
            IsAssigned = glAccount.IsAssigned;
            Balance = glAccount.Balance;
        }
    }
   
}
