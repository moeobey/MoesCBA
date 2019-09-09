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
    public class UsersController : Controller
    {
        private readonly UserLogic _context = new UserLogic();
        // GET: Users
        [CheckSession]
        public ActionResult Index()
        {
            var users = _context.GetAllWithBranch();
            
                return View("Dashboard", users);
        }
        [CheckSession]
        public ActionResult ChangePassword()
        {
            return View("ChangePasswordForm");
        }

        [CheckSession]
        [CheckRole]
        public ActionResult New()
        {
            var branches = _context.GetBranches().ToList();
            
            //var roles = _context.GetRoles().ToList();
            var viewModel = new NewUserViewModel
            {
                
                Branches =  branches,
                //UserRoles =  roles
            };

            return View("UserForm",viewModel);
        }
        [CheckSession]
        [CheckRole]
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
                    user.Role = "Teller";
                    user.Date = DateTime.Now;
                    _context.Save(user);
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
                Branches = _context.GetBranches().ToList()

            };

         
            //ViewBag.msg = "Incorrect Username or Password";
            return View("UserForm", viewModel);
        }
        [HttpPost]
        [CheckSession]
        public ActionResult UpdatePassword(User user)
        {
            if (Session["id"] != null)
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
                        TempData["message"] = "password successfully changed";
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


            return RedirectToAction("LogOut", "Account");
        }


        [CheckSession]
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
        [CheckSession]
        [CheckRole]
        [HttpPost]
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
                    //var userInDb = _context.Get(user.Id);
                    userInDb.FullName = user.FullName;
                    userInDb.Email = user.Email;
                    userInDb.BranchId = user.BranchId;
                    userInDb.PhoneNumber = user.PhoneNumber;
                    _context.Update(user);

                    TempData["message"] = "Update Successful";
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







       
        
    }
}