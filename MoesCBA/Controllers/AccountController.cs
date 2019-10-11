using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CBA.Core.Implementation;
using CBA.Logic;
using NLog;

namespace MoesCBA.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountLogic _context = new AccountLogic();
        private readonly UserLogic _userContext = new UserLogic();
        private  readonly  BankConfigLogic _bankConfigContext = new BankConfigLogic();
        // GET: Account
        public ActionResult Index()
        {
            if (Session["Id"] != null)
            {
                return RedirectToAction("Index", "Users");
            }
          
            return View("LoginForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AuthenticateUser(User user)
        {
            var curUser = _context.GetByUsername(user.Username);
            if (_context.Authenticate(user))
            {
                //authentication successful
                Session["id"] = curUser.Id;
                Session["Password"] = curUser.Password;
                Session["Username"] = curUser.Username;
                Session["Role"] = curUser.Role;
                if (_bankConfigContext.GetConfig() != null)//if bank has been set up create session for financial date
                {
                    Session["FinancialDate"] = _bankConfigContext.GetConfig().FinancialDate;
                }
                if ((string) Session["Role"] == $"Admin")
                {
                    var bankConfig = _bankConfigContext.GetConfig();
                    if (bankConfig == null)//if bank configuration is not set show the button to setup bank
                    {
                        Session["setup"] = "start";
                    }
                    else
                    {
                        Session["isBusinessOpen"] = bankConfig.IsBusinessOpen;
                    }

                    return RedirectToAction("AdminDashboard", "Users");
                }
                if (curUser.PasswordStatus == false) 
                {
                    //user using default password
                    return RedirectToAction("ChangePassword", "Users");
                }
                if ((string)Session["Role"] == $"Teller" && curUser.PasswordStatus)
                {
                    //user has changed password
                    return RedirectToAction("TellerDashboard", "Users");
                }
            }
            ViewBag.msg = "Incorrect Username or Password";
            return View("LoginForm");
        }

        [CheckSession]
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Account");
        }


    }
}