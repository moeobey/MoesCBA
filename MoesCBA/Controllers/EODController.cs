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
    public class EODController : Controller
    {
        private readonly BankConfigLogic _bankConfigContext = new BankConfigLogic();
        private readonly EodLogic _context = new EodLogic();
        private readonly TransactionLogLogic _transactionContext = new TransactionLogLogic();


        // GET: EOD
        public ActionResult Index()
        {
            //var transactions = _transactionContext.GetAllLogs();
         
            return View();
        }
        [CheckRole]

        public ActionResult CloseBusiness()
        {
            var bankConfig = _bankConfigContext.GetConfig();
            if (bankConfig.IsBusinessOpen)
            {
                bankConfig.IsBusinessOpen = false;
                _bankConfigContext.Update(bankConfig);
                Session["isBusinessOpen"] = false;
            }
            //run rod when business is closed
            var runEod = _context.RunEod();
            if (runEod !="Success")
            {
                TempData["Error"] = runEod;
            }
            else
            {
                TempData["Success"] = "EOD run Successful";
                Session["FinancialDate"] = _bankConfigContext.GetConfig().FinancialDate;
            }
            return RedirectToAction("Index", "BankConfig");


        }

        [CheckRole]

        public ActionResult OpenBusiness()
        {
            var bankConfig = _bankConfigContext.GetConfig();
            if (!bankConfig.IsBusinessOpen)
            {
                bankConfig.IsBusinessOpen = true;
                _bankConfigContext.Update(bankConfig);
                Session["isBusinessOpen"] = true;
            }
            return RedirectToAction("Index", "BankConfig");

        }
    }
}