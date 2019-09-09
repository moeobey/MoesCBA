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
    public class GlAccountsController : Controller
    {
        private readonly GlAccountLogic _context = new GlAccountLogic();
        private readonly UserLogic _userContext = new UserLogic();
       
        [CheckSession]
        [CheckRole]
        public ActionResult Index()
        {
            var glAccounts = _context.GetAllGlAccounts();
            return View(glAccounts);
        }
        [CheckSession]
        [CheckRole]
        public ActionResult New()
        {
            var branches = _userContext.GetBranches();
            var glAccountCategories = _context.GetGlAccountCategories();
            var viewModel = new NewGlAccountViewModel()
            {
             Branches = branches,
             GlAccountCategories = glAccountCategories
            };
            return View("GlAccountForm", viewModel);
        }

        [CheckSession]
        [CheckRole]
        public ActionResult Save(GlAccount account)
        {
            if (ModelState.IsValid)
            {
                var accountInDb = _context.Get(account.Id);
                if (account.Id == 0)
                {
                    account.AccountCode = _context.GenerateGlAccountCode(account.GlAccountCategoryId);
                    _context.Save(account);
                    TempData["message"] = "GL Account Added Successfully";
                    return RedirectToAction("Index");
                }

                if (account.Id != 0)   //update account
                {
                    accountInDb.Name = account.Name;
                    accountInDb.BranchId = account.BranchId;
                    _context.Update(account);
                    TempData["message"] = "Update Successful";
                    return RedirectToAction("Index");
                }
            }
            var branches = _userContext.GetBranches();
            var glAccountCategories = _context.GetGlAccountCategories();
            var tAccount = new NewGlAccountViewModel()
            {
                Branches = branches,
                GlAccountCategories = glAccountCategories
            };
            return View("GlAccountForm", tAccount);
        }


        [CheckSession]
        [CheckRole]
        public ActionResult Edit(int id)
        {
            var branches = _userContext.GetBranches().ToList();
            var glAccount = _context.Get(id);
            var glAccountCategories = _context.GetGlAccountCategories();
            var viewModel = new NewGlAccountViewModel(glAccount)
            {
                Branches = branches,
                GlAccountCategories = glAccountCategories
            };
            return View("GlAccountForm", viewModel);
        }
    }
}