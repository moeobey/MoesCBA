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
      
        public IEnumerable<TransactionLog> TransactionLogs { get; set; }

        public decimal DebitTotal { get; set; }
        public decimal CreditTotal { get; set; }

    }
}
