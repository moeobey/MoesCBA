using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using CBA.Data;
using CBA.Data.Implementation;

namespace CBA.Logic
{
   public class CustomerLogic
    {
        private readonly CustomerRepository _db = new CustomerRepository(new ApplicationDbContext());
        public Customer Get(int id)
        {
            var values = _db.Get(id);
            return values;
        }
        public void Update(Customer customer)
        {
            _db.Save(customer);
        }

        public void Save(Customer customer)
        {
            _db.Add(customer);
            _db.Save(customer);
        }
        public IEnumerable<Customer> GetAll()
        {
            var values = _db.GetAll();
            return values;
        }
        public bool PhoneNumberIsUnique(string phoneNumber)
        {
            bool isUnique = false;

            var user = _db.GetByPhoneNumber(phoneNumber);
            if (user == null)
                isUnique = true;

            return isUnique;
        }
        public bool EmailIsUnique(string userMail)
        {
            bool isUnique = false;

            var email = _db.GetByEmail(userMail);
            if (email == null)
                isUnique = true;

            return isUnique;
        }

        public string GenerateCustomerId()
        {
            var ids = _db.GetAll().OrderByDescending(b => b.Id);
            var customerId = "0000001";
            if (ids.Count() > 1)
            {
                var lastCustomerId = ids.First().Id;
                lastCustomerId++;
                customerId = lastCustomerId.ToString().PadLeft(6, '0');
            }
            return customerId;
        }

        public Customer GetByCustomerId(string customerId)
        {
           
                var values = _db.GetByCustomerId(customerId);
                return values;
            
        }
    }
}
