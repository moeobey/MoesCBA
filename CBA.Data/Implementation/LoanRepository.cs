using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CBA.Core.Implementation;

namespace CBA.Data.Implementation
{
   public class LoanRepository:Repository<Loan>
    {
        readonly ApplicationDbContext _context;
        public LoanRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Loan> GetAllLoans()
        {

            return _context.Loans.Include(c =>c.CustomerAccount).ToList();
        }
        public IEnumerable<Loan> GetAllUnpaidLoans()
        {

            return _context.Loans.Where(c=>c.IsLoanPaymentComplete == false).Include(c => c.CustomerAccount).ToList();
        }

        public Loan GetByCustomerId(int id)
        {
            return _context.Loans.Include(c=>c.CustomerAccount).FirstOrDefault(c => c.CustomerAccountId == id);
        }



    }
}
