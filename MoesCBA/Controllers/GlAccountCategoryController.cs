using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBA.Core.Implementation;
using CBA.Logic;

namespace MoesCBA.Controllers
{
    public class GlAccountCategoryController : Controller
    {
        private readonly GlAccountCategoryLogic _context = new GlAccountCategoryLogic();
        // GET: GlAccountCategory
        [CheckSession]
        [CheckRole]
        public ActionResult Index()
        {
            var categories = _context.GetAll();
            return View(categories);
        }
        [CheckSession]
        [CheckRole]
        public ActionResult New()
        {
            var category = new GlAccountCategory
            {
                Id = 0
            };
            return View("GlAccountCategoryForm", category);
        }
        [CheckSession]
        [CheckRole]
        public ActionResult Save( GlAccountCategory category)
        {
            if (ModelState.IsValid)
            {
                var categoryInDb = _context.Get(category.Id);
                if (category.Id == 0)
                {
                    _context.Save(category);
                    TempData["message"] = "Category Added Successfully";
                    return RedirectToAction("Index");
                }
                if (category.Id != 0)   //update Category
                {
                        categoryInDb.Name = category.Name;
                        categoryInDb.Description = category.Description;

                        _context.Update(category);
                        TempData["message"] = "Update Successful";
                    return RedirectToAction("Index");
                }
            }
            var tCategory = new GlAccountCategory
            {
                Id = 0
            };
            return View("GlAccountCategoryForm", tCategory);
        }
        [CheckSession]
        [CheckRole]
        public ActionResult Edit(int id)
        {
            var category = _context.Get(id);
            if (category == null)
                return HttpNotFound();

            return View("GlAccountCategoryForm", category);
        }

    }
}