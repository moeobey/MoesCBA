using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBA.Core.Implementation;
using CBA.Core.ViewModels;
using CBA.Logic;
using MoesCBA.Models;

namespace MoesCBA.Controllers
{
    [CheckRole]
    [CheckSession]
    public class GlPostsController : Controller
    {
        private readonly  GlPostingLogic _context = new GlPostingLogic();
        private readonly  GlAccountLogic _glAccountContext = new GlAccountLogic();
        
        // GET: GlPosts
        public ActionResult Index()
        {
            var glPosts = _context.GetAllPosts();
            return View(glPosts);
        }
        [CheckBusinessOpen]

        public ActionResult New()
        {
            var glAccounts = _glAccountContext.GetAll();
            var viewModel = new GlPostViewModel
            {
                GlAccounts = glAccounts
            };

            return View("GlPostForm", viewModel);
        }

        [CheckBusinessOpen]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Save(GlPost glPost)
        {
            
            if (glPost.GlAccountToCreditId == glPost.GlAccountToDebitId)
            {
                TempData["error"] = "You cannot Post to the same account";
                var glAccounts = _glAccountContext.GetAll();
                var viewModel = new GlPostViewModel
                {
                    GlAccounts = glAccounts
                };
                return View("GlPostForm", viewModel);
            }

            var postEntry =  _context.PostEntry(glPost);
            
           if (postEntry == "Success")
           {
               glPost.PostedAt = DateTime.Now;
               glPost.UserId = Convert.ToInt32(Session["id"]);
               _context.Save(glPost);
               TempData["success"] = "gl Posted Successfully";
           }
            else //if gl post fails,  Show error
            {
                 TempData["error"] = postEntry;
                 var glAccounts = _glAccountContext.GetAll();
                 var viewModel = new GlPostViewModel
                 {
                     GlAccounts = glAccounts
                 };
                 return View("GlPostForm", viewModel);
            }
         
            return RedirectToAction("Index");
        }
    }
}