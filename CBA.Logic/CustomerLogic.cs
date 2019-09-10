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
            var customerId = _db.GetAll().OrderByDescending(b => b.Id);
            var lastUserId = 1;
            if (customerId.Count() > 1)
                lastUserId = customerId.First().Id;
            
            Random rnd = new Random();
            int newRand = rnd.Next(1, 100);
            var value = "19" + lastUserId.ToString() + newRand.ToString();
            return value;

        }
    }
}
