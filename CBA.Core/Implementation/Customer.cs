using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBA.Core.Implementation
{
    public enum Gender
{
    Male =1 ,
    Female  
}
    public class Customer
    {
      
            public int Id { get; set; }
            public string CustomerId { get; set; }
            public string FullName { get; set; }

            public string Address { get; set; }

            public string Email { get; set; }

            
            public string PhoneNumber { get; set; }

            public Gender Gender { get; set; }

            public DateTime? Date { get; set; }

          


    }
}

