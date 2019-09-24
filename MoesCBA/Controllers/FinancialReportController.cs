using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBA.Core.ViewModels;
using CBA.Logic;

namespace MoesCBA.Controllers
{
    public class FinancialReportController : Controller
    {
        private readonly TransactionReportLogic _context = new TransactionReportLogic();
        private readonly BankConfigLogic _bankContext = new BankConfigLogic();
        private readonly TransactionLogLogic _transactionContext = new TransactionLogLogic();
        // GET: FinancialReport
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BalanceSheet()
        {
            var assetAccounts = _context.GetAllAssetAccounts();
            var liabilityAccounts = _context.GetAllLiabilityAccounts();
            var capitalAccounts = _context.GetAllCapitalAccounts();
            var assetTotal = assetAccounts.Sum(c=>c.Balance);
            var liabilityTotal = liabilityAccounts.Sum(c=>c.Balance);
            var capitalTotal = capitalAccounts.Sum(c=>c.Balance);
            var clSum = liabilityTotal + capitalTotal; //sum of capital and liability, to compare with asset


            var viewModel = new BalanceSheetViewModel
            {
                AssetAccounts = assetAccounts,
                LiabilityAccounts = liabilityAccounts,
                CapitalAccounts = capitalAccounts,
                AssetTotal =  assetTotal,
                LiabilityTotal = liabilityTotal,
                CapitalTotal = capitalTotal,
                CLSum = clSum
            };
            return View(viewModel);
        }

        public ActionResult PLReport()
        {
            var incomeAccounts = _context.GetAllIncomeAccounts();
            var expenseAccounts = _context.GetAllExpenseAccounts();
            var incomeTotal = incomeAccounts.Sum(c => c.Balance);
            var expenseTotal = expenseAccounts.Sum(c => c.Balance);

            var viewModel = new PlViewModel
            {
                IncomeGls = incomeAccounts,
                ExpenseGls = expenseAccounts,
                IncomeTotal =  incomeTotal,
                ExpenseTotal =  expenseTotal,
                Profit =  incomeTotal - expenseTotal,
                Date = _bankContext.GetConfig().FinancialDate
            };
            return View(viewModel);
        }

        public ActionResult TrialBalance()
        {
            var debitTransactions = _transactionContext.GetDebitLogs();
            var transactions = _transactionContext.GetAllLogs();
            var creditTransactions = _transactionContext.GetCreditLogs();
            var creditTotal = creditTransactions.Sum(c => c.Amount);
            var debitTotal = debitTransactions.Sum(c => c.Amount);
            var viewModel = new TrialBalanceViewModel
            {
                DebitTotal = debitTotal,
                TransactionLogs = transactions,
                CreditTotal = creditTotal
            };
            return View(viewModel);
        }

    }
}