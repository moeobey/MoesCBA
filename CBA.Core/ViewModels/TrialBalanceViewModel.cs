using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Core.ViewModels
{
    public class TrialBalanceViewModel
    {

        public MainAccountCategory MainCategory { get; set; }
        public string AccountName { get; set; }
        public  long AccountCode { get; set; }
        public decimal DebitTotal { get; set; }
        public decimal CreditTotal { get; set; }

    }
}
