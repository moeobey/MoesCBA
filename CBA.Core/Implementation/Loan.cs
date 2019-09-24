using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.Implementation
{
    public enum RepaymentPlan
    {
        Daily = 30
    }

    public enum LoanTerms
    {
        Fixed = 1, 
        Reducing = 2
    }
   public class Loan
    {
        public int Id { get; set; }
        public CustomerAccount CustomerAccount { get; set; }
        public int CustomerAccountId { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal LoanAmountRemaining { get; set; }
        public decimal LoanPayable { get; set; }
        public decimal Interest { get; set; }
        public decimal InterestRemaining { get; set; }
        public decimal InterestPayable { get; set; }
        public int DurationInMonths { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int DayCount { get; set; }
        public RepaymentPlan RepaymentPlan { get; set; }

        public LoanTerms? LoanTerms { get; set; }
        public string Narration { get; set; }
        public bool IsLoanPaymentComplete { get; set; }

    }
}


