using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBA.Core.Implementation;
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
                LiabilityAccounts =liabilityAccounts,
                CapitalAccounts = capitalAccounts,
                AssetTotal =  Math.Round(assetTotal,2),
                LiabilityTotal = Math.Round(liabilityTotal,2),
                CapitalTotal = Math.Round(capitalTotal,2),
                CLSum = Math.Round(clSum, 2)
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
                IncomeTotal =  Math.Round(incomeTotal,2),
                ExpenseTotal =  Math.Round(expenseTotal,2),
                Profit = Math.Round( incomeTotal - expenseTotal,2),
                Date = _bankContext.GetConfig().FinancialDate
            };
            return View(viewModel);
        }

        public ActionResult TrialBalance(string startDate, string endDate)
        {
            var transactions = _transactionContext.GetAllLogs().ToList();
            if (startDate !=null && endDate !=null)//get trial balance by date
            {
                transactions = _transactionContext.GetLogsByPeriod(startDate, endDate).OrderBy(c=>c.MainAccountCategory).ToList();
            }
            var viewModel = new List<TrialBalanceViewModel>();
            foreach (var transaction in transactions)
            {
                var model = viewModel.FirstOrDefault(t => t.AccountCode == transaction.AccountCode);
                if (model == null) //the transaction is unique
                {
                    model = new TrialBalanceViewModel
                    {
                        AccountCode = transaction.AccountCode,
                        AccountName = transaction.Name,
                        MainCategory =  transaction.MainAccountCategory,
                        DebitTotal = transaction.TransactionType==TransactionType.Debit?transaction.Amount:0,
                        CreditTotal = transaction.TransactionType==TransactionType.Credit?transaction.Amount:0,
                    };
                    viewModel.Add(model);
                }
                else
                {
                    //the account code is not unique
                    var amount = transaction.Amount;
                    if (transaction.TransactionType == TransactionType.Debit)
                    {
                        model.DebitTotal += amount;
                        //creditTotal += amount;
                    }
                    else 
                    {
                        model.CreditTotal += amount;
                        //debitTotal += amount;
                    }
                }

            }

            ViewBag.DebitTotal = viewModel.Sum(c=>c.DebitTotal);
            ViewBag.CreditTotal = viewModel.Sum(c=>c.CreditTotal);
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            return View(viewModel);

        }
        

    }
}