using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.Implementation
{
    public enum MainAccountCategory {
        Asset = 1, 
        Liability = 2,
        Capital = 3,
        Income = 4, 
        Expense = 5

    }
   public class GlAccountCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MainAccountCategory MainAccountCategory { get; set; }
        public string Description { get; set; }

    }
}
