using System;
using System.CodeDom;
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
        public string GetCustomerId(int id)
        {

            var values = _db.GetCustomerId(id);
            return values;
        }
        public void Update(CustomerAccount account)
        {
            _db.Save(account);
        }
        public string GenerateAccountNumber(string customerId, string accountType)
        {
            switch (accountType)
            {
                case "Savings":
                    return "1" + customerId;
                case "Current":
                    return "2" + customerId;
                case "Loan":
                    return "3" + customerId;
                default:
                    return "";
            }

        }


    }
}
