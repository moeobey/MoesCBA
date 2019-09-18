using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.Implementation
{
   public class BankConfiguration
    {
        public int Id { get; set; }
        public DateTime FinancialDate { get; set; }
        public bool IsBusinessOpen { get; set; }

        public int DayCount { get; set; }
        public int MonthCount { get; set; }

        public int YearCount { get; set; }

    }
}
