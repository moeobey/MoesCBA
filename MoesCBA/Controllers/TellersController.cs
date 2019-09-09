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
    public class TellersController : Controller
    {
        private readonly UserLogic _userContext = new UserLogic();
        private readonly GlAccountLogic _glAccountContext = new GlAccountLogic();
        private  readonly  TellerLogic _context = new TellerLogic();
        // GET: Tellers
        [CheckSession]
        [CheckRole]
        public ActionResult Index()
        {
            var tellers = _context.GetWithAll();
            return View(tellers);
        }
        [CheckSession]
        [CheckRole]
        public ActionResult New(int id)
        {
            //if (id != 0)
            //{
                var selectedUser = _userContext.GetUnassigned(id);
                var selectedGlAccounts = _glAccountContext.GetAllUnassigned();
                var selectedViewModel = new NewTellerViewModel
                {
                    User = selectedUser,
                    GlAccount = selectedGlAccounts

                };
                return View("TellerForm", selectedViewModel);
            //}
            //var users = _userContext.GetAllUnassigned();
            //var glAccounts = _glAccountContext.GetAllUnassigned();
            //var viewModel = new NewTellerViewModel
            //{
            //    User =  users,
            //    GlAccount = glAccounts
            //};
            //return View("TellerForm", viewModel);
        }

        [CheckSession]
        [CheckRole]
        public ActionResult Save(Teller teller)
        {
            var user = new User();
            var glAccount = new GlAccount();
            var userInDb = _userContext.Get(Convert.ToInt32(teller.UserId));
            var glAccountInDb = _glAccountContext.Get(teller.GlAccountId);
            if (ModelState.IsValid)
            {
                userInDb.IsAssigned = true;
                _userContext.Update(user);

                glAccountInDb.IsAssigned = true;
                _glAccountContext.Update(glAccount);

                _context.Save(teller);
                TempData["message"] = "Till Account Assigned Successfully";
                return RedirectToAction("Index");
            }

            var users = _userContext.GetAllUnassigned();
            var glAccounts = _glAccountContext.GetAllUnassigned();
            var viewModel = new NewTellerViewModel
            {
                User = users,
                GlAccount = glAccounts
            };
            return View("TellerForm", viewModel);

        }


    }
}