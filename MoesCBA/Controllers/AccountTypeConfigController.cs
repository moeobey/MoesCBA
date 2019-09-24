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
            object interestPayableGl = null;
            if (config != null)
            {
                 glAccount = _glAccountContext.Get(config.InterestExpenseGlId); //to get the gl account name
                 interestPayableGl = _glAccountContext.Get(Convert.ToInt32(config.InterestPayableGlId));

            }
            var glAccounts = _glAccountContext.GetAllExpenseAccount();
            var interestPayableGls = _glAccountContext.GetAllLiabilityAccount();
            var viewModel = new SavingsAccountConfigViewModel(config)
            {
                InterestExpenseGl = (GlAccount) glAccount,
                InterestPayableGl = (GlAccount) interestPayableGl,
                InterestExpenseGls = glAccounts,
                InterestPayableGls = interestPayableGls
                
            };
            
           return View("SavingsAccountTypeForm", viewModel);
        }

        [HttpPost]
        public ActionResult SaveSavingsConfig(SavingsAccountConfig config)
        {
            
            var glAccount = new GlAccount();
            var configInDb = _context.GetSavingsConfig();
            var glAccountInDb = _glAccountContext.Get(config.InterestExpenseGlId);
            if (config.Id == 0) //setup new configuration
            {
                glAccountInDb.IsAssigned = true; // Update the gl account to is assigned
                _glAccountContext.Update(glAccount);

                config.Status = true;
                _context.SaveSavings(config);
                TempData["message"] = "Configurations Added Successfully";
                return RedirectToAction("SavingsAccount");

            }

            if (config.Id != 0) //update current savings configuration
            {
                configInDb.CInterestRate = config.CInterestRate;
                configInDb.MinBalance = config.MinBalance;
                configInDb.InterestPayableGlId = config.InterestPayableGlId;

                if (config.InterestExpenseGlId == 0) //check if gl account is the same
                {
                    configInDb.InterestExpenseGlId = configInDb.InterestExpenseGlId;
                }
                else
                {
                    var newGlAccountInDb = _glAccountContext.Get(configInDb.InterestExpenseGlId); // free up the old gl account that was assigned
                    newGlAccountInDb.IsAssigned = false;
                    _glAccountContext.Update(glAccount);

                    configInDb.InterestExpenseGlId = config.InterestExpenseGlId;

                    glAccountInDb.IsAssigned = true; // Update the  new gl account to is assigned
                    _glAccountContext.Update(glAccount);

                }
                TempData["message"] = "Configurations Updated Successfully";
                _context.UpdateSavings(config);
            }
            return RedirectToAction("SavingsAccount");
        }

        public ActionResult CurrentAccount()
        {
            var config = _context.GetCurrentConfig();
            object interestExpenseGl = null;
            object interestPayableGl = null;
            object COTincomeGl = null;
            if (config != null)
            {
                interestExpenseGl = _glAccountContext.Get(config.InterestExpenseGlId); //to get the gl account name
                interestPayableGl = _glAccountContext.Get(Convert.ToInt32(config.InterestPayableGlId));
                COTincomeGl = _glAccountContext.Get(config.COTIncomeGlId);
            }
            var interestExpenseGls = _glAccountContext.GetAllExpenseAccount();
            var interestPayableGls = _glAccountContext.GetAllLiabilityAccount();
            var COTincomeGls = _glAccountContext.GetAllIncomeAccount();
            var viewModel = new CurrentAccountConfigViewModel(config)
            {
                InterestExpenseGl = (GlAccount)interestExpenseGl,
                InterestExpenseGls = interestExpenseGls,
                InterestPayableGls =  interestPayableGls,
                COTIncomeGl = (GlAccount) COTincomeGl,
                COTIncomeGls = COTincomeGls
            };

            return View("CurrentAccountTypeForm", viewModel);
        }

        [HttpPost]
        public ActionResult SaveCurrentConfig(CurrentAccountConfig config)
        {

            var glAccount = new GlAccount();
            var configInDb = _context.GetCurrentConfig();
            var interestExpenseGlInDb = _glAccountContext.Get(config.InterestExpenseGlId);
            var cotIncomeGlInDb = _glAccountContext.Get(config.COTIncomeGlId);
            if (config.Id == 0) //setup new configuration
            {
                interestExpenseGlInDb.IsAssigned = true; // Update the Interest gl account to is assigned
                cotIncomeGlInDb.IsAssigned = true;// update the income gl account
                _glAccountContext.Update(glAccount);

                config.Status = true;               //change the status of configuration to true
                _context.SaveCurrent(config);
                return RedirectToAction("CurrentAccount");
            }

            if (config.Id != 0) //update current configuration
            {
                configInDb.CInterestRate = config.CInterestRate;
                configInDb.MinBalance = config.MinBalance;
                configInDb.COT = config.COT;
                configInDb.InterestPayableGlId = config.InterestPayableGlId;

                if (config.InterestExpenseGlId == 0) //check if any of the  gl accounts are the same
                {
                    configInDb.InterestExpenseGlId = configInDb.InterestExpenseGlId;
                  
                }
                else
                {
                    var oldInterestExpenseGlInDb = _glAccountContext.Get(configInDb.InterestExpenseGlId); // free up the old gl accounts that was assigned
                    oldInterestExpenseGlInDb.IsAssigned = false;
                    _glAccountContext.Update(glAccount);

                    configInDb.InterestExpenseGlId = config.InterestExpenseGlId;


                    interestExpenseGlInDb.IsAssigned = true; // Update the  new gl account to is assigned
                    _glAccountContext.Update(glAccount);

                }

                if (config.COTIncomeGlId == 0)
                {
                    configInDb.COTIncomeGlId = configInDb.COTIncomeGlId;
                }
                else
                {
                    var oldIncomeGlInDb = _glAccountContext.Get(configInDb.COTIncomeGlId);
                    oldIncomeGlInDb.IsAssigned = false;
                    _glAccountContext.Update(glAccount);


                    configInDb.COTIncomeGlId = config.COTIncomeGlId;
                    cotIncomeGlInDb.IsAssigned = true;
                    _glAccountContext.Update(glAccount);

                }


                _context.UpdateCurrent(config);
                return RedirectToAction("CurrentAccount");
            }
            return RedirectToAction("CurrentAccount");
        }


        public ActionResult LoanAccount()
        {
            var config = _context.GetLoanConfig();
            object interestIncomeGl = null;
            if (config != null)
            {
                interestIncomeGl = _glAccountContext.Get(config.InterestIncomeGlId); //to get the gl account name

            }
            var interestIncomeGls = _glAccountContext.GetAllIncomeAccount();
            var viewModel = new LoanAccountConfigViewModel(config)
            {
                InterestIncomeGl = (GlAccount)interestIncomeGl,
                InterestIncomeGls = interestIncomeGls
            };

            return View("LoanAccountTypeForm", viewModel);
        }

        [HttpPost]
        public ActionResult SaveLoanConfig(LoanAccountConfig config)
        {

            var interestIncomeGl = new GlAccount();
            var configInDb = _context.GetLoanConfig();
            var interestIncomeGlInDb = _glAccountContext.Get(config.InterestIncomeGlId);
            if (config.Id == 0) //setup new configuration
            {
                interestIncomeGlInDb.IsAssigned = true; // Update the gl account to is assigned
                _glAccountContext.Update(interestIncomeGl);

                config.Status = true;
                _context.SaveLoan(config);
                return RedirectToAction("LoanAccount");
            }

            if (config.Id != 0) //update current configuration
            {
                configInDb.DInterestRate = config.DInterestRate;
                if (config.InterestIncomeGlId == 0) //check if gl account is the same
                {
                    configInDb.InterestIncomeGl = configInDb.InterestIncomeGl;
                }
                else
                {
                    var oldGlAccountInDb = _glAccountContext.Get(configInDb.InterestIncomeGlId); // free up the old gl account that was assigned
                    oldGlAccountInDb.IsAssigned = false;
                    _glAccountContext.Update(interestIncomeGl);

                    configInDb.InterestIncomeGlId = config.InterestIncomeGlId;


                    interestIncomeGlInDb.IsAssigned = true; // Update the  new gl account to is assigned
                    _glAccountContext.Update(interestIncomeGl);

                }

                _context.UpdateLoan(config);
                return RedirectToAction("LoanAccount");
            }
            return RedirectToAction("LoanAccount");
        }
    }
}