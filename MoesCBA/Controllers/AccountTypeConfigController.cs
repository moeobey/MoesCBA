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
    [CheckRole]
    [CheckSession]
    public class AccountTypeConfigController : Controller
    {
        private readonly AccountTypeConfigLogic _context = new AccountTypeConfigLogic();
        private readonly  GlAccountLogic _glAccountContext  = new GlAccountLogic();

        // GET: AccountTypeConfig
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SavingsAccount()
        {
            var config = _context.GetSavingsConfig();
            object glAccount = null;
            if (config != null)
            {
                 glAccount = _glAccountContext.Get(config.GlAccountId); //to get the gl account name

            }
            var glAccounts = _glAccountContext.GetAllExpenseAccount();
            var viewModel = new SavingsAccountConfigViewModel(config)
            {
                GlAccount = (GlAccount) glAccount,
                GlAccounts = glAccounts
            };
            
           return View("SavingsAccountTypeForm", viewModel);
        }

        [HttpPost]
        public ActionResult SaveSavingsConfig(SavingsAccountConfig config)
        {
            
            var glAccount = new GlAccount();
            var configInDb = _context.GetSavingsConfig();
            var glAccountInDb = _glAccountContext.Get(config.GlAccountId);
            if (config.Id == 0) //setup new configuration
            {
                glAccountInDb.IsAssigned = true; // Update the gl account to is assigned
                _glAccountContext.Update(glAccount);

                _context.SaveSavings(config);
                return RedirectToAction("SavingsAccount");
            }

            if (config.Id != 0) //update current configuration
            {
                configInDb.CInterestRate = config.CInterestRate;
                configInDb.MinBalance = config.MinBalance;
                if (config.GlAccountId == 0) //check if gl account is the same
                {
                    configInDb.GlAccountId = configInDb.GlAccountId;
                }
                else
                {
                    var newGlAccountInDb = _glAccountContext.Get(configInDb.GlAccountId); // free up the old gl account that was assigned
                    newGlAccountInDb.IsAssigned = false;
                    _glAccountContext.Update(glAccount);

                    configInDb.GlAccountId = config.GlAccountId;

                  
                    glAccountInDb.IsAssigned = true; // Update the  new gl account to is assigned
                    _glAccountContext.Update(glAccount);

                }
                
                _context.UpdateSavings(config);
                return RedirectToAction("SavingsAccount");
            }
            return RedirectToAction("SavingsAccount");
        }

        public ActionResult CurrentAccount()
        {
            var config = _context.GetCurrentConfig();
            object interestExpenseGl = null;
            object COTincomeGl = null;
            if (config != null)
            {
                interestExpenseGl = _glAccountContext.Get(config.InterestExpenseGlId); //to get the gl account name
                COTincomeGl = _glAccountContext.Get(config.COTIncomeGlId);

            }
            var interestExpenseGls = _glAccountContext.GetAllExpenseAccount();
            var COTincomeGls = _glAccountContext.GetAllIncomeAccount();
            var viewModel = new CurrentAccountConfigViewModel(config)
            {
                InterestExpenseGl = (GlAccount)interestExpenseGl,
                InterestExpenseGls = interestExpenseGls,
                COTIncomeGl = (GlAccount) COTincomeGl,
                COTIncomeGls = COTincomeGls
            };

            return View("CurrentAccountTypeForm", viewModel);
        }

        //[HttpPost]
        //public ActionResult SaveCurrentConfig(CurrentAccountConfig config)
        //{

        //    var glAccount = new GlAccount();
        //    var configInDb = _context.GetSavingsConfig();
        //    var glAccountInDb = _glAccountContext.Get(config.GlAccountId);
        //    if (config.Id == 0) //setup new configuration
        //    {
        //        glAccountInDb.IsAssigned = true; // Update the gl account to is assigned
        //        _glAccountContext.Update(glAccount);

        //        _context.SaveSavings(config);
        //        return RedirectToAction("SavingsAccount");
        //    }

        //    if (config.Id != 0) //update current configuration
        //    {
        //        configInDb.CInterestRate = config.CInterestRate;
        //        configInDb.MinBalance = config.MinBalance;
        //        if (config.GlAccountId == 0) //check if gl account is the same
        //        {
        //            configInDb.GlAccountId = configInDb.GlAccountId;
        //        }
        //        else
        //        {
        //            var newGlAccountInDb = _glAccountContext.Get(configInDb.GlAccountId); // free up the old gl account that was assigned
        //            newGlAccountInDb.IsAssigned = false;
        //            _glAccountContext.Update(glAccount);

        //            configInDb.GlAccountId = config.GlAccountId;


        //            glAccountInDb.IsAssigned = true; // Update the  new gl account to is assigned
        //            _glAccountContext.Update(glAccount);

        //        }

        //        _context.UpdateSavings(config);
        //        return RedirectToAction("SavingsAccount");
        //    }
        //    return RedirectToAction("SavingsAccount");
        //}

     
        public ActionResult LoanAccount()
        {
            return View("LoanAccountTypeForm");
        }
    }
}