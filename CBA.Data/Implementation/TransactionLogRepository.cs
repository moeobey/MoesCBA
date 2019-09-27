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

            return _context.TransactionLogs.OrderBy(c=>c.MainAccountCategory)
                .ToList();
        }
        public IEnumerable<TransactionLog> GetLogsByPeriod(string startDate, string endDate)
        {
            var start = Convert.ToDateTime(startDate);
            var end = Convert.ToDateTime(endDate);
            var transactions  = new List<TransactionLog>();
            if (start <= end)
            {
                var logs = GetAllLogs().ToList();
                foreach (var log in logs)
                {
                    if (log.Date.Date >= start && log.Date.Date <= end)
                    {
                        transactions.Add(log);
                    }
                }
            }

            return transactions;

        }


    }
}
