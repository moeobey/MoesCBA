using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CBA.Core.Implementation;
using CBA.Logic;
using  CBA.Core.ViewModels;

namespace MoesCBA.Controllers
{
        [CheckSession]
        public class UsersController : Controller
        {
        
        private readonly UserLogic _context = new UserLogic();
        private readonly CustomerAccountLogic _customerAccContext  = new CustomerAccountLogic();
        private readonly TransactionReportLogic _transactionReportContext  = new TransactionReportLogic();
        private readonly GlAccountLogic _glAccountContext  = new GlAccountLogic();
        private readonly LoanLogic _loanContext  = new LoanLogic();
        private readonly TellerPostLogic _tellerContext = new TellerPostLogic();
        public ActionResult AdminDashboard()
        {
            var numberOfCustomerAccounts = 0;
            var numberOfGlAccounts = 0;
            var numberOfLoans = 0;
            decimal profit = 0;

            if (_customerAccContext.GetAllCustomersAccounts() != null)
            {
                 numberOfCustomerAccounts = _customerAccContext.GetAllCustomersAccounts().Count();
            }

            if (_glAccountContext.GetAll() != null)
            {
                numberOfGlAccounts = _glAccountContext.GetAll().Count();
            }
            if (_loanContext.GetAllLoans() != null)
            {
                numberOfLoans = _loanContext.GetAllLoans().Count();
            }

            if (_transactionReportContext.GetProfitOrLoss() != 0)
            {
                 profit = _transactionReportContext.GetProfitOrLoss();
            }
            var viewModel = new AdminDashboardViewModel
            {
                NumberOfCustomerAccounts = numberOfCustomerAccounts,
                Profit = profit,
                NumberOfGlAccounts = numberOfGlAccounts,
                NumberOfLoans = numberOfLoans,
            };
            return View("AdminDashboard", viewModel);
        } public ActionResult TellerDashboard()
        {
            var numberOfTellerPosting = 0;
            decimal tillBalance = 0;

            if (_tellerContext.GetAllPosts(Convert.ToInt32(Session["Id"])) != null)
            {
                numberOfTellerPosting = _tellerContext.GetAllPosts(Convert.ToInt32(Session["Id"])).Count();
            }

            if (_tellerContext.GetTillBalance(Convert.ToInt32(Session["Id"])) != 0)
            {
                tillBalance = _tellerContext.GetTillBalance(Convert.ToInt32(Session["Id"]));
            }

            var viewModel = new TellerDashboardViewModel
            {
                NumberOfPosting = numberOfTellerPosting,
                TillAccountBalance =  tillBalance,
            };
            return View("TellerDashboard", viewModel );
        }
        public ActionResult Index()
        {
            var users = _context.GetAllWithBranch(); //get the branch along with the users
            
                return View("Index", users);
        }
        [CheckSession]
        public ActionResult ChangePassword()
        {
            return View("ChangePasswordForm");
        }

        [CheckRole]
        public ActionResult New()
        {
            var branches = _context.GetBranches().ToList();
            var roles = _context.GetRoles().ToList();
            var viewModel = new NewUserViewModel
            {
                Branches =  branches,
                UserRoles = roles
            };

            return View("UserForm",viewModel);
        }

        [CheckRole]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(User user)
        {
            if (ModelState.IsValid)
            {
                var usernameIsUnique = _context.UserIsUnique(user.Username);
                var emailIsUnique = _context.EmailIsUnique(user.Email);
                if (usernameIsUnique && emailIsUnique)
                {
                    var randomPass = _context.GenerateRandomPassword();
                    user.Password = Crypto.Hash(_context.GenerateRandomPassword());
                    user.Date = DateTime.Now;
                    _context.Save(user);
                    TempData["Success"] = $"{user.Role} Added changed";
                    _context.SendEmail(user.Email, randomPass ,user.FullName);
                    return RedirectToAction("Index");
                }
                if (!usernameIsUnique)
                {
                    ModelState.AddModelError("username", "Username is Taken");
                }
                if (!emailIsUnique)
                {
                    ModelState.AddModelError("email", "Email Already Exist");
                }
            }
            var viewModel = new NewUserViewModel
            {
                UserRoles = _context.GetRoles().ToList(),
                Branches = _context.GetBranches().ToList()

            };

            ViewBag.msg = "Incorrect Username or Password";
            return View("UserForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePassword(User user)
        {
                var userInDb = _context.Get((int)Session["id"]);
                if (String.CompareOrdinal(userInDb.Password, Crypto.Hash(user.Password.Trim())) == 0)
                {
                    var newPassword = Request.Form["newPassword"];
                    var confirmPassword = Request.Form["confirmPassword"];
                    if (String.CompareOrdinal(newPassword, confirmPassword) == 0)
                    {
                        userInDb.Password = Crypto.Hash(newPassword);
                        userInDb.PasswordStatus = true;
                        _context.Update(user);
                        TempData["Success"] = "password successfully changed";
                        return RedirectToAction("Index", "Users");
                    }
                    else
                    {
                        ModelState.AddModelError("PasswordMismatch", "Passwords do not match, Try Again");
                        return View("ChangePasswordForm");
                    }
                }
                else
                {
                    ModelState.AddModelError("PasswordIncorrect", "Incorrect Password");
                    return View("ChangePasswordForm");
                }
        }

        [CheckRole]
        public ActionResult Edit(int id)
        {
            var user = _context.Get(id);
            if (user == null)
                return HttpNotFound();
            var branches = _context.GetBranches().ToList();
            var viewModel = new NewUserViewModel
            {
                User =  user,
                Branches = branches,
            };
            return View("EditUserForm", viewModel);
        }

        [CheckRole]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUserDetails(User user)
        {
            var userInDb = _context.Get(user.Id);
            var emailIsUnique = _context.EmailIsUnique(user.Email);

            if (user.Email == userInDb.Email)
            {
                emailIsUnique = true;
            }
            if (emailIsUnique)
            {
                userInDb.FullName = user.FullName;
                    userInDb.Email = user.Email;
                    userInDb.BranchId = user.BranchId;
                    userInDb.PhoneNumber = user.PhoneNumber;
                    _context.Update(user);

                    TempData["Success"] = "Update Successful";
                    return RedirectToAction("Index");
            }
            if (!emailIsUnique)
            {
                    ModelState.AddModelError("EmailExist", "Email Already Exist");
            }
            
            var viewModel = new NewUserViewModel
            {
                User = user,
                Branches = _context.GetBranches().ToList()
            };
            return View("EditUserForm", viewModel);
        }

        public ActionResult Details(int id)
        {
            var user = _context.GetCurrentUser(id);
            return View(user);
        }

    }
}