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
    [CheckSession]
    public class TellerPostsController : Controller
    {
        private readonly CustomerAccountLogic _customerAccContext = new CustomerAccountLogic();
        private readonly  TellerPostLogic _context = new TellerPostLogic();
        private readonly  TellerLogic _tellerContext = new TellerLogic();
        private readonly GlAccountLogic _glAccountContext = new GlAccountLogic();



        // GET: TellerPosts
        public ActionResult Index()
        {
            var customerAccounts = _customerAccContext.GetAllCustomersAccounts();
           var glId = _tellerContext.GetTillAccount(Convert.ToInt32(Session["Id"]));
            ViewBag.GlBalance = _glAccountContext.GetByAccCode(glId).Balance;   //for the tellers till balance

            return View(customerAccounts);
        }

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
        public ActionResult Save(TellerPost tellerPost)
        { 
            tellerPost.CustomerAccountId = Convert.ToInt32(Request.Form["accountId"]);
            tellerPost.TellerId = Convert.ToInt32(Session["Id"]);
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
                var customerAccount = _customerAccContext.Get(tellerPost.CustomerAccountId);
                var viewModel = new TellerPostViewModel
                {
                    CustomerAccount = customerAccount,
                };
                return View("TellerPostForm", viewModel);
            }

            return RedirectToAction("Index");
        }

    }
}