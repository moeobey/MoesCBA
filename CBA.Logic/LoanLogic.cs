using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using CBA.Data;
using CBA.Data.Implementation;

namespace CBA.Logic
{
  public  class LoanLogic
    {
        private readonly LoanRepository _db = new LoanRepository(new ApplicationDbContext());
        public IEnumerable<Loan> GetAllLoans( )
        {
            return _db.GetAllLoans();
        }
        public void Save(Loan loan)
        {
            _db.Add(loan);
            _db.Save(loan);
        }
        public void Update(Loan account)
        {
            _db.Save(account);
        }

    }
}
