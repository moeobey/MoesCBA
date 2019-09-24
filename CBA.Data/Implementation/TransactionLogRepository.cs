using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using  System.Data.Entity;

namespace CBA.Data.Implementation
{
    public class TransactionLogRepository : Repository<TransactionLog>
    {
        readonly ApplicationDbContext _context;

        public TransactionLogRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
        public IEnumerable<TransactionLog> GetAllLogs()
        {

            return _context.TransactionLogs
                .ToList();
        }
        public List<TransactionLog> GetDebitLogs()
        {

            return _context.TransactionLogs.Where(c=>c.TransactionType == TransactionType.Debit)
                .ToList();
        }
        public List<TransactionLog> GetCreditLogs()
        {

            return _context.TransactionLogs
                .Where(c => c.TransactionType == TransactionType.Debit)
                .ToList();
        }
        

    }
}
