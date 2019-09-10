using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.Implementation
{
   public class TillAccount
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public GlAccount GlAccount { get; set; }
        public int GlAccountId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
