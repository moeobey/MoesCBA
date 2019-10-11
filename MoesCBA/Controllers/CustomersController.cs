using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Mvc;
using CBA.Core.Implementation;
using CBA.Core.ViewModels;
using CBA.Logic;

namespace MoesCBA.Controllers
{
    [CheckSession]
    [CheckRole]
    public class CustomersController : Controller
    {
        private readonly CustomerLogic _context = new CustomerLogic();
        // GET: CustomersController
        public ActionResult Index()
        {
            var users = _context.GetAll();
            return View(users);
        }
        public ActionResult New()
        {
            var customer = new Customer
            {
                Id = 0
            };
            return View("CustomerForm", customer);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (ModelState.IsValid)
            {

                var customerInDb = _context.Get(customer.Id);
                var emailIsUnique = _context.EmailIsUnique(customer.Email);
                var phoneNumberIsUnique = _context.PhoneNumberIsUnique(customer.PhoneNumber);
                if (customer.Id != 0 && customerInDb.Email == customer.Email ) //check if Email is Unchanged for update and set is unique to true
                    emailIsUnique = true;  
                
                if (customer.Id != 0 && customerInDb.PhoneNumber == customer.PhoneNumber) //Check if phone number is unchanged for update and set is unique to true
                    phoneNumberIsUnique = true;
                
                if (phoneNumberIsUnique && emailIsUnique)
                {
                    if (customer.Id != 0)   //update customer
                    {
                        customerInDb.FullName = customer.FullName;
                        customerInDb.Address = customer.Address;
                        customerInDb.Email = customer.Email;
                        customerInDb.PhoneNumber = customer.PhoneNumber;
                        customerInDb.Gender = customer.Gender;
                        //customer.
                        _context.Update(customer);
                        TempData["Success"] = "Update Successful";
                        return RedirectToAction("Index");
                    }

                    if ( customer.Id == 0) //add customer
                    {
                        customer.CustomerId = _context.GenerateCustomerId();
                        customer.Date = DateTime.Now;
                        _context.Save(customer);
                        TempData["Success"] = "Customer Successfully Added";

                        return RedirectToAction("Index");
                    }
                  
                }
                if (!emailIsUnique)
                    ModelState.AddModelError("EmailExist", "This Email Exists");

                if (!phoneNumberIsUnique)
                    ModelState.AddModelError("PhoneNumberExist", "This Phone Number Exists");
            }

            var currentCustomer = new Customer
            {
                Id = 0
            };
            return View("CustomerForm", currentCustomer);
        }
        public ActionResult Edit(int id)
        {
            var customer= _context.Get(id);
            if (customer == null)
                return HttpNotFound();
            
            return View("CustomerForm", customer);
        }
        public ActionResult Details(int id)
        {
            var customer = _context.Get(id);
            return View(customer);
        }

    }
}