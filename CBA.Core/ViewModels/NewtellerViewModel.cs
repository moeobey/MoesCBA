using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Core.ViewModels
{
  public  class NewTellerViewModel
    {
        public User User { get; set; }
        public IEnumerable<GlAccount> GlAccount { get; set; }
        public  Teller Teller { get; set; }
      
    }
}
