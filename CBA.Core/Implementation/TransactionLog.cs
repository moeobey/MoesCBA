using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.Implementation
{
    public enum TransactionType
    {
        Debit, Credit
    }
   public class TransactionLog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MainAccountCategory MainAccountCategory { get; set; }
        public long AccountCode { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }

        public DateTime Date { get; set; }
    }
}
