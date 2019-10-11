    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CBA.Core.Implementation
{
    public class CheckSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var mySession = HttpContext.Current.Session;
            if (mySession["id"] == null)
            {
                filterContext.Result = new RedirectResult(string.Format("/home/"));
            }
        }
    }
}
