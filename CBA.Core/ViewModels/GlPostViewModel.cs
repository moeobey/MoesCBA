using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Core.ViewModels
{
   public class GlPostViewModel
    {

        public IEnumerable<GlAccount> GlAccounts { get; set; }

        public GlPost GlPost { get; set; }

    }
}
