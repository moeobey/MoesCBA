using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBA.Core.Implementation
{
    public class User: IPerson
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime? Date { get; set; }
        public bool PasswordStatus { get; set; }
        public string Role { get; set; }
        public Branch Branch { get; set; }
        public int? BranchId { get; set; }
        public bool IsAssigned { get; set; }
    }
}
