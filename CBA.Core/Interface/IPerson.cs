using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.Interface
{
    public interface IPerson
    {
         int Id { get; set; }
         string FullName { get; set; }

         string BranchId { get; set; }
         DateTime Date { get; set; }
    }
}
