using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CBA.Core.Implementation;
using CBA.Data;
using CBA.Data.Implementation;
using CBA.Logic;


namespace MoesCBA.Models
{
    public  class CheckBusinessOpen: ActionFilterAttribute
    {
        private   BankConfigLogic _context = new BankConfigLogic();


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_context == null)
            {
                _context = new BankConfigLogic();
            }
            var bankConfig = _context.GetConfig();
            _context = null;

            if (bankConfig == null || !bankConfig.IsBusinessOpen)
            {
                
                filterContext.Result = new RedirectResult(string.Format("/BankConfig/"));
            }
            
        }
    }
}
