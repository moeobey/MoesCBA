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
        // GET: Account
        public ActionResult Index()
        {
            if (Session["Id"] != null)
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
             return View("LoginForm");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AuthenticateUser(User user)
        {
            //validate model state
            var curUser = _context.GetByUsername(user.Username);
            if (_context.Authenticate(user))
            {
                Session["id"] = curUser.Id;
                Session["Password"] = curUser.Password;
                Session["FullName"] = curUser.FullName;
                Session["Role"] = curUser.Role;
                if ((string) Session["Role"] == $"Admin")
                {
                    return RedirectToAction("Index", "Users");
                }
                else if ((string) Session["Role"] == $"Teller" && curUser.PasswordStatus == false) 
                {
                    return RedirectToAction("ChangePassword", "Users");
                    //change password page
                    //return RedirectToAction("Index", "Users");
                }
                else if ((string)Session["Role"] == $"Teller" && curUser.PasswordStatus == true)
                {
                    //user has changed password
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    //user has  no role
                    return RedirectToAction("Index", "Account");
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