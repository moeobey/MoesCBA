using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBA.Core.Implementation;
using CBA.Logic;

namespace MoesCBA.Controllers
{
    [CheckSession]

    public class BankConfigController : Controller
    {

        private  readonly  BankConfigLogic _context = new BankConfigLogic();
        //GET: BankConfig
        public ActionResult Index()
        {
            var bankConfig = _context.GetConfig();
            return View(bankConfig);
        }
        [CheckRole]
        public ActionResult SetupConfig()
        {
           var bankConfig = new BankConfiguration
            {
                FinancialDate = DateTime.Today,
                IsBusinessOpen = false,
                DayCount = 0,
                MonthCount = 0,
                YearCount = 0
            };
            Session["isBusinessOpen"] = false;
            Session.Remove("setup");
            _context.Save(bankConfig);
            return RedirectToAction("Index");
        }
    }
}