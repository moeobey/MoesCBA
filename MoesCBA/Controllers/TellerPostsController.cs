using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBA.Core.Implementation;
using CBA.Core.ViewModels;
using CBA.Logic;
using MoesCBA.Models;

namespace MoesCBA.Controllers
{
    [CheckSession]
    public class TellerPostsController : Controller
    {
        private readonly CustomerAccountLogic _customerAccContext = new CustomerAccountLogic();
        private readonly  TellerPostLogic _context = new TellerPostLogic();
        private readonly  TellerLogic _tellerContext = new TellerLogic();
        private readonly GlAccountLogic _glAccountContext = new GlAccountLogic();
        private readonly AccountTypeConfigLogic _accountTypeConfigContext = new AccountTypeConfigLogic();



        // GET: TellerPosts
        public ActionResult Index()
        {
            var customerAccounts = _customerAccContext.GetOpenAccounts();
            ViewBag.GlBalance = _context.GetTillBalance(Convert.ToInt32(Session["Id"]));
            return View(customerAccounts);
        }

        public ActionResult ViewPosts()
        {
            ViewBag.GlBalance = _context.GetTillBalance(Convert.ToInt32(Session["Id"]));
            var posts = _context.GetAllPosts(Convert.ToInt32(Session["Id"]));
            return View(posts);
        }
        [CheckBusinessOpen]
        public ActionResult New(int id)
        {
            var customerAccount = _customerAccContext.Get(id);
            var viewModel = new TellerPostViewModel
            {
                CustomerAccount =  customerAccount,
            };
            return View("TellerPostForm", viewModel);
        }
        [HttpPost]
        [CheckBusinessOpen]
        public ActionResult Save(TellerPost tellerPost)
        { 
            tellerPost.CustomerAccountId = Convert.ToInt32(Request.Form["accountId"]);
            tellerPost.TellerId = Convert.ToInt32(Session["Id"]);
            var customerAccount = _customerAccContext.Get(Convert.ToInt32(Request.Form["accountId"]));
            var currentConfig = _accountTypeConfigContext.GetCurrentConfig();
            if (currentConfig == null)      //check if current account configurations have been set
            {
                TempData["error"] = "Account Configuration Not Set";
                var viewModel = new TellerPostViewModel
                {
                    CustomerAccount = customerAccount,
                };
                return View("TellerPostForm", viewModel);
            }
            //check if customer account is open

            if (!customerAccount.IsOpen)
            {
                TempData["error"] = "Customer Account is Closed Set";
                var viewModel = new TellerPostViewModel
                {
                    CustomerAccount = customerAccount,
                };
                return View("TellerPostForm", viewModel);
            }
            var postEntry = _context.PostEntry(tellerPost);
            if (postEntry == "Success")
            {
                tellerPost.PostedAt = DateTime.Now;
                _context.Save(tellerPost);
                TempData["Success"] = "Post Successful";
            }
            else
            {
                TempData["error"] = postEntry;
                
                var viewModel = new TellerPostViewModel
                {
                    CustomerAccount = customerAccount,
                };
                return View("TellerPostForm", viewModel);
            }

            return RedirectToAction("Index");
        }
        [CheckBusinessOpen]

        public ActionResult BuyCash()
        {
            ViewBag.GlBalance = _context.GetTillBalance(Convert.ToInt32(Session["Id"]));
            return View("BuyCashForm");
        }
        [CheckBusinessOpen]

        public ActionResult SellCash()
        {
            ViewBag.GlBalance = _context.GetTillBalance(Convert.ToInt32(Session["Id"]));
            return View("SellCashForm");
        }
        [CheckBusinessOpen]

        [HttpPost]
        public ActionResult Buy()
        {
            var tellerId = Convert.ToInt32(Session["id"]);
           var  amount = Convert.ToDecimal(Request.Form["amount"]);
            var buyCash = _context.BuyCash(amount, tellerId);
            if (buyCash == "Success")
            {
                TempData["Success"] = "Buy Cash Successful";
                return RedirectToAction("ViewPosts");

            }
            else
            {
                TempData["Error"] = buyCash;
                return View("BuyCashForm");
            }
        }
        [HttpPost]
        [CheckBusinessOpen]

        public ActionResult Sell()
        {
            var tellerId = Convert.ToInt32(Session["id"]);
            var amount = Convert.ToDecimal(Request.Form["amount"]);
            var sellCash = _context.SellCash(amount, tellerId);
            if (sellCash == "Success")
            {
                TempData["Success"] = "Sell Cash Successful";
                return RedirectToAction("ViewPosts");

            }
            else
            {
                TempData["Error"] = sellCash;
                return View("SellCashForm");
            }
        }


    }
}