using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;

namespace CBA.Data.Implementation
{
   public class CustomerRepository: Repository<Customer>
    {
        readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
       
        public Customer GetByPhoneNumber(string phoneNumber)
        {
            var tPhoneNumber= _context.Customer.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
            return tPhoneNumber;

        }
        public Customer GetByEmail(string email)
        {
            var tEmail = _context.Customer.FirstOrDefault(u => u.Email == email);
            return tEmail;

        }

    }
}
