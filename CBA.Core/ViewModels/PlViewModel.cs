using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Core.ViewModels
{
   public class PlViewModel
    {
        public IEnumerable<GlAccount> IncomeGls { get; set; }
        public IEnumerable<GlAccount> ExpenseGls { get; set; }
        public decimal IncomeTotal { get; set; }
        public decimal ExpenseTotal { get; set; }
        public decimal Profit { get; set; }
        public DateTime Date { get; set; }
    }
}
