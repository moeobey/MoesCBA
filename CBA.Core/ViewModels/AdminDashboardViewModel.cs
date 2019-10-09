using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int NumberOfCustomerAccounts { get; set; }
        public int NumberOfGlAccounts { get; set; }
        public int NumberOfLoans { get; set; }

        public decimal Profit { get; set; }
    }
}
