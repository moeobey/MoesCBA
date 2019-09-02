using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBA.Logic;

namespace MoesCBA.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserLogic _context = new UserLogic();
        // GET: Users
        public ActionResult Index()
        {
            var users = _context.GetAll();
            return View(users);
        }
    }
}