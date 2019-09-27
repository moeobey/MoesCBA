using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBA.Core.Implementation;
using CBA.Logic;

namespace MoesCBA.Controllers
{
    public class LoanController : Controller
    {
        private  readonly  LoanLogic _context = new LoanLogic();
        private  readonly  AccountTypeConfigLogic _accountTypeConfigContext = new AccountTypeConfigLogic();
        private  readonly  CustomerAccountLogic _customerAccContext = new CustomerAccountLogic();

     //   GET: Loan
        public ActionResult Index()
        {
            var loans = _context.GetAllLoans();
            return View(loans);
        }

        public ActionResult New(int id)
        {
            var customerAccount = _customerAccContext.Get(id);
            var loan = new Loan
            {
                CustomerAccount = customerAccount
            }; 
            return View("LoanForm", loan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Loan loan)
        {
            var customerAccount = _customerAccContext.Get(Convert.ToInt32(Request.Form["customerId"]));

            var isLoanComplete = _customerAccContext.CheckForLoanStatus(customerAccount);
           if (!isLoanComplete)
           {
               TempData["Error"] = "Customer Has Unpaid Loan (Click Here To Review)";

                var currentLoan = new Loan
               {
                   CustomerAccount = customerAccount
               };
               return View("LoanForm", currentLoan);
           }
           else
           {


               var dInterestRate = _accountTypeConfigContext.GetLoanConfig().DInterestRate;

               //Fill Loan Account Details
               loan.Name = customerAccount.AccountName;
               loan.AccountNumber = _customerAccContext.GenerateAccountNumber(customerAccount.CustomerId, "Loan");
               loan.CustomerAccountId = customerAccount.Id;
               loan.StartDate = DateTime.Today;
               loan.EndDate = DateTime.Now.AddDays(loan.DurationInMonths * 30);
               loan.Interest = loan.LoanAmount * (dInterestRate / 100) *
                               (Convert.ToDecimal(loan.DurationInMonths) / 12);
               loan.InterestRemaining = loan.Interest;
               loan.InterestPayable = loan.Interest / (loan.DurationInMonths * 30);
               loan.LoanAmountRemaining = loan.LoanAmount; // debiting Loan Account
               loan.LoanPayable = loan.LoanAmount / (loan.DurationInMonths * 30);
               loan.DayCount = 0;
               _context.Save(loan);


               //create customer Loan Account
               var customerLoanAcc = new CustomerAccount
               {
                   CustomerId = customerAccount.CustomerId,
                   AccountName = customerAccount.AccountName,
                   AccountNumber = loan.AccountNumber,
                   BranchId = customerAccount.BranchId,
                   Balance = 0,
                   AccountType = AccountType.Loan,
                   IsOpen = true,
                   CreatedAt = DateTime.Now,
               };



               _customerAccContext.CreditCustomer(customerAccount, loan.LoanAmount);
               _customerAccContext.DebitCustomer(customerLoanAcc, loan.LoanAmount);

               _customerAccContext.Save(customerLoanAcc);
           }

           TempData["Success"] = "Loan Taken Successfully";
           return RedirectToAction("Index");
        }

        public ActionResult Details(int id )
        {
            var loan = _context.GetByCustomerId(id);
            return View(loan);
        }
    }
}