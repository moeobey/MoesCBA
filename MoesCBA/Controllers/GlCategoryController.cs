using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBA.Core.Implementation;
using CBA.Logic;

namespace MoesCBA.Controllers
{
    [CheckSession]
    [CheckRole]
    public class GlCategoryController : Controller
    {
        private readonly GlCategoryLogic _context = new GlCategoryLogic();
        // GET: GlAccountCategory
       
        public ActionResult Index()
        {
            var categories = _context.GetAll();
            return View(categories);
        }
        public ActionResult New()
        {
            var category = new GlCategory
            {
                Id = 0
            };
            return View("GlCategoryForm", category);
        }
        public ActionResult Save( GlCategory category)
        {
            if (ModelState.IsValid)
            {
                var categoryInDb = _context.Get(category.Id);
                if (category.Id == 0 && category.MainAccountCategory != 0)
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

                if (category.MainAccountCategory == 0)
                {
                
                    ModelState.AddModelError("mainGl", "please select a category ");
                    
                }
            }
            var tCategory = new GlCategory
            {
                Id = 0
            };
            return View("GlCategoryForm", tCategory);
        }
        public ActionResult Edit(int id)
        {
            var category = _context.Get(id);
            if (category == null)
                return HttpNotFound();

            return View("GlCategoryForm", category);
        }
    }
}