﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Interface;

namespace CBA.Core.Implementation
{
    public class User: IPerson
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string BranchId { get; set; }

        public string Email { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
        public DateTime Date { get; set; }

    }
}
