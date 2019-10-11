using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.Implementation
{
  
    public class GlAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GlCategory GlCategory { get; set; }
        public int GlCategoryId { get; set; }
        public Branch Branch { get; set; }
        public int BranchId { get; set; }
        public long AccountCode { get; set; }
        public bool IsAssigned { get; set; }
        public decimal Balance { get; set; }
        public  bool IsTillAccount { get; set; }
    }
}