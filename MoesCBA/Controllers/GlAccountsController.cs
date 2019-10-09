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
    public class GlAccountsController : Controller
    {
        private readonly GlAccountLogic _context = new GlAccountLogic();
        private readonly UserLogic _userContext = new UserLogic();
        public ActionResult Index()
        {
            var glAccounts = _context.GetAllGlAccounts();
            return View(glAccounts);
        }
        public ActionResult New()
        {
            var branches = _userContext.GetBranches();
            var glCategories = _context.GetGlCategories();
            var viewModel = new NewGlAccountViewModel()
            {
             Branches = branches,
             GlCategories = glCategories
            };
            return View("GlAccountForm", viewModel);
        }
        public ActionResult Save(GlAccount account)
        {
            if (ModelState.IsValid)
            {
                var accountInDb = _context.Get(account.Id);
                var glAccountNameIsUnique = _context.NameIsUnique(account.Name) || account.Id != 0 && accountInDb.Name == account.Name; //check if account Name is Unchanged for update and set is unique to true

                if (account.Id == 0 && glAccountNameIsUnique) //add account
                {
                    if (Request.Form["tillAccount"] == "on")
                    {
                        account.IsTillAccount = true;
                    }
                    account.AccountCode = _context.GenerateGlAccountCode(account.GlCategoryId);
                    _context.Save(account);
                    TempData["Success"] = "GL Account Added Successfully";
                    return RedirectToAction("Index");
                }

                if (account.Id != 0 && glAccountNameIsUnique)   //update account
                {
                    accountInDb.Name = account.Name;
                    accountInDb.BranchId = account.BranchId;
                    _context.Update(account);
                    TempData["Success"] = "Update Successful";
                    return RedirectToAction("Index");
                }

                if (!glAccountNameIsUnique)
                {
                    ModelState.AddModelError("nameTaken", "This Name Exist");
                }
            }

            var branches = _userContext.GetBranches();
            var glCategories = _context.GetGlCategories();
            var tAccount = new NewGlAccountViewModel()
            {
                Branches = branches,
                GlCategories = glCategories
            };
            return View("GlAccountForm", tAccount);
        }
        public ActionResult Edit(int id)
        {
            var branches = _userContext.GetBranches().ToList();
            var glAccount = _context.Get(id);
            var glCategories = _context.GetGlCategories();
            var viewModel = new NewGlAccountViewModel(glAccount)
            {
                Branches = branches,
                GlCategories = glCategories
            };
            return View("GlAccountForm", viewModel);
        }
        public ActionResult Details(int id)
        {
            var account = _context.GetGlAccount(id);
            return View(account);
        }
    }
}