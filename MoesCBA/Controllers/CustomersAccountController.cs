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
    public class CustomersAccountController : Controller
    {
        private readonly CustomerLogic _customerContext = new CustomerLogic();
        private readonly UserLogic _userContext = new UserLogic();

        private readonly CustomerAccountLogic _context = new CustomerAccountLogic();
        // GET: CustomersAccount
     
        public ActionResult Index()
        {
            var customerAccounts = _context.GetAllCustomersAccounts();

            return View(customerAccounts);
        }
      
        public ActionResult New(int id)
        {
            var customer = _customerContext.Get(id);
            var branches = _userContext.GetBranches().ToList();
           
            var viewModel = new NewCustomerAccountViewModel
            {
                Customer =  customer,
                Branches = branches

            };
            return View("CustomerAccountForm", viewModel);
        }
        
        public ActionResult Save(CustomerAccount account)
        {
            int userId = Convert.ToInt32(Request.Form["userId"]);
            int id = Convert.ToInt32(Request.Form["id"]);
            if (ModelState.IsValid)
            {
                var accountInDb = _context.Get(account.Id);
                if (id== 0 && account.AccountType !=0)
                {
                    var customerId = Request.Form["customerId"];
                    account.CustomerId = customerId;
                    //account.AccountNumber = "111111";
                    account.CreatedAt = DateTime.Now;
                    account.IsOpen = true;

                    account.AccountNumber = _context.GenerateAccountNumber(customerId, account.AccountType.ToString());
                    _context.Save(account);
                    TempData["message"] = "Account Created Successfully";
                    return RedirectToAction("Index");
                }
                if (account.Id != 0)   //update account
                {
                    accountInDb.AccountName = account.AccountName;
                    accountInDb.BranchId = account.BranchId;
                    accountInDb.Lien = account.Lien;
                    _context.Update(account);
                    TempData["message"] = "Update Successful";
                    return RedirectToAction("Index");
                }

                if (account.AccountType == 0)
                {
                    ModelState.AddModelError("selectAccountType", "Please select Account Type");
                    
                }
            }
            var customer = _customerContext.Get(userId);
            var branches = _userContext.GetBranches().ToList();

            var viewModel = new NewCustomerAccountViewModel
            {
                Customer = customer,
                Branches = branches

            };
            return View("CustomerAccountForm", viewModel);

        }
        public ActionResult Edit(int id)
        {
            var customerId = _context.GetCustomerId(id);
            var branches = _userContext.GetBranches().ToList();
            var customerAccount = _context.Get(id);
            var customer = _customerContext.GetByCustomerId(customerId);
            var viewModel = new NewCustomerAccountViewModel(customerAccount)
            {
                Branches = branches,
                Customer =  customer
               
            };
            return View("CustomerAccountForm", viewModel);
        }

     
        public JsonResult CloseAccount(int id)
        {
            var customerAccount = new CustomerAccount();
            bool result = false;
            var customer = _context.Get(id);
            if (customer != null)
            {
                customer.IsOpen = false;
                _context.Update(customerAccount);
                result = true;
                TempData["message"] = "Account Closed Successfully";
            }
            return Json(result, JsonRequestBehavior.AllowGet);


        }
      
        public JsonResult OpenAccount(int id)
        {
            var customerAccount = new CustomerAccount();
            bool result = false;
            var customer = _context.Get(id);
            if (customer != null)
            {
                customer.IsOpen = true;
                _context.Update(customerAccount);
                result = true;
                TempData["message"] = "Account Opened Successfully";

            }
            return Json(result, JsonRequestBehavior.AllowGet);


        }
    }
}