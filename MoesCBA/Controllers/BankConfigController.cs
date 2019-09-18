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
    [CheckRole]
    public class BankConfigController : Controller
    {
        private  readonly  BankConfigLogic _context = new BankConfigLogic();
        //GET: BankConfig
        public ActionResult Index()
        {
            var bankConfig = _context.GetConfig();
            return View(bankConfig);
        }

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
    
        public ActionResult CloseBusiness()
        {
            var bankConfig = _context.GetConfig();
            if (bankConfig.IsBusinessOpen )
            {
                bankConfig.IsBusinessOpen = false;
                _context.Update(bankConfig);
                Session["isBusinessOpen"] = false;

            }

            return RedirectToAction("Index");
        }

        //[HttpPost]
        public ActionResult OpenBusiness()
        {
            var bankConfig = _context.GetConfig();
            if (!bankConfig.IsBusinessOpen)
            {
                bankConfig.IsBusinessOpen = true;
                _context.Update(bankConfig);
                Session["isBusinessOpen"] = true;

            }
            return RedirectToAction("Index");

        }
    }
}