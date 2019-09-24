using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Core.ViewModels
{
   public class BalanceSheetViewModel
    {
        public IEnumerable<GlAccount> AssetAccounts { get; set; }
        public IEnumerable<GlAccount> CapitalAccounts { get; set; }
        public IEnumerable<GlAccount> LiabilityAccounts { get; set; }
        public decimal AssetTotal { get; set; }

        public decimal CapitalTotal { get; set; }

        public decimal LiabilityTotal { get; set; }

        public decimal CLSum { get; set; }



    }
}
