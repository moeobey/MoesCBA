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
    public class CustomerAccountLogic
    {
        private readonly CustomerAccountRepository _db = new CustomerAccountRepository(new ApplicationDbContext());
        
        public void Save(CustomerAccount customer)
        {
            //Console.WriteLine("trying to add user");
            _db.Add(customer);
            _db.Save(customer);
        }
        public CustomerAccount Get(int id)
        {

            var values = _db.Get(id);
            return values;
        }
        public IEnumerable<CustomerAccount> GetAllCustomersAccounts()
        {
            var value = _db.GetAllCustomersAccounts();
            return value;
        }
    }
}
