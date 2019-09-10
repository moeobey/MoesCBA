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
    public class CustomersAccountController : Controller
    {
        private readonly CustomerLogic _customerContext = new CustomerLogic();
        private readonly UserLogic _userContext = new UserLogic();

        private readonly CustomerAccountLogic _context = new CustomerAccountLogic();
        // GET: CustomersAccount
        [CheckSession]
        [CheckRole]
        public ActionResult Index()
        {
            var customerAccounts = _context.GetAllCustomersAccounts();

            return View(customerAccounts);
        }
        [CheckSession]
        [CheckRole]
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
        [CheckSession]
        [CheckRole]
        public ActionResult Save(CustomerAccount account)
        {
            int userId = Convert.ToInt32(Request.Form["userId"]);
            int id = Convert.ToInt32(Request.Form["id"]);
            if (ModelState.IsValid)
            {
                if (id== 0)
                {
                    account.CustomerId = Request.Form["customerId"];
                    account.AccountNumber = "111111";
                    account.CreatedAt = DateTime.Now;
                    //account.AccountNumber = _context.generateAccountNumber(account.Customer, account.AccountType);
                    _context.Save(account);
                    TempData["message"] = "Account Created Successfully";
                    return RedirectToAction("Index");
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
        [CheckSession]
        [CheckRole]
        public ActionResult Edit(int id)
        {
            var branches = _userContext.GetBranches().ToList();
            var customerAccount = _context.Get(id);
            var customer = _customerContext.Get(id);
            var viewModel = new NewCustomerAccountViewModel(customerAccount)
            {
                Branches = branches,
                Customer =  customer
               
            };
            return View("CustomerAccountForm", viewModel);
        }

    }
}