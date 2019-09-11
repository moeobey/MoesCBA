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
    [CheckRole]
    public class TellersController : Controller
    {
        private readonly UserLogic _userContext = new UserLogic();
        private readonly GlAccountLogic _glAccountContext = new GlAccountLogic();
        private  readonly  TellerLogic _context = new TellerLogic();
        // GET: Tellers
        public ActionResult Index()
        {
            var tellers = _context.GetWithAll();
            return View(tellers);
        }
        public ActionResult New(int id)
        {
                var selectedUser = _userContext.Get(id);
                var selectedGlAccounts = _glAccountContext.GetAllUnassigned();
                var selectedViewModel = new NewTellerViewModel
                {
                    User = selectedUser,
                    GlAccount = selectedGlAccounts

                };
                return View("TellerForm", selectedViewModel);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Teller teller)
        {
            int userId = Convert.ToInt32(Request.Form["userId"]);
            if (_userContext.GetUnassigned(userId) != null)
            {
                var user = new User();
                var glAccount = new GlAccount();


                var userInDb = _userContext.Get(userId);
                var glAccountInDb = _glAccountContext.Get(teller.GlAccountId);

                userInDb.IsAssigned = true;
                _userContext.Update(user);

                glAccountInDb.IsAssigned = true;
                _glAccountContext.Update(glAccount);

                teller.UserId = userId;
                _context.Save(teller);
                TempData["message"] = "Till Account Assigned Successfully";
                return RedirectToAction("Index");
            }

            ViewBag.hasTillAccount = "User Has Till Account";
            var tellers = _context.GetWithAll();
            var selectedUser = _userContext.Get(userId);
            var selectedGlAccounts = _glAccountContext.GetAllUnassigned();
            var selectedViewModel = new NewTellerViewModel
            {
                User = selectedUser,
                GlAccount = selectedGlAccounts

            };
            return View("TellerForm", selectedViewModel);


        }


    }
}