using System.Web.Mvc;

namespace MoesCBA.Controllers
{
    public class HomeController : Controller
    {
        //[HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public ActionResult AnotherLink()
        {
            return View("Index");
        }
    }
}
