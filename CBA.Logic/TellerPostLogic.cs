using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using CBA.Data;
using CBA.Data.Implementation;

namespace CBA.Logic
{
  public  class TellerPostLogic
    {
        private  readonly  TellerPostRepository _db = new TellerPostRepository( new ApplicationDbContext());
        private readonly GlAccountLogic _glAccountContext = new GlAccountLogic();
        private readonly CustomerAccountLogic _customerAccountContext = new CustomerAccountLogic();
        private readonly TellerLogic _tellerContext = new TellerLogic();
        private  readonly  AccountTypeConfigLogic _configContext = new AccountTypeConfigLogic();
        private readonly COTPostRepository _COTPostContext = new COTPostRepository(new ApplicationDbContext());

        public void Save(TellerPost tellerPost)
        {
            _db.Add(tellerPost);
            _db.Save(tellerPost);
        }
        public IEnumerable<TellerPost> GetAllPosts(int tellerId)
        {
            return _db.GetAllPosts(tellerId);
        }
        private void DebitGl(long tillAccount, decimal amount)
        {
            var gl = new GlAccount();
            var glAccount = _glAccountContext.GetByAccCode(tillAccount);
            glAccount.Balance = glAccount.Balance + amount;
            _glAccountContext.Update(gl);
        }
        private void CreditGl(long tillAccount, decimal amount)
        {
            var account = new GlAccount();
            var glAccount = _glAccountContext.GetByAccCode(tillAccount);
            glAccount.Balance = glAccount.Balance - amount;
            _glAccountContext.Update(account);
        }
        private void DebitCustomer(CustomerAccount customerAccount, decimal amount)
        {
            var customer = new CustomerAccount();
            customerAccount.Balance = customerAccount.Balance - amount;
            _customerAccountContext.Update(customer);
        }
        private void CreditCustomer(CustomerAccount customer, decimal amount)
        {
            var account = new CustomerAccount();
            customer.Balance += amount;
            _customerAccountContext.Update(account);
        }
        private void CreditIncomeGl(int incomeGlId, decimal  COT )
        {
            var account = new GlAccount();
            var glAccount = _glAccountContext.Get(incomeGlId);
            glAccount.Balance = glAccount.Balance + COT;
            _glAccountContext.Update(account);
        }
        private  decimal CalculateCOT(decimal cot, decimal amount)
        {
            var result = (amount / 1000) * cot;
            return result;
        }
        private void RemoveCOT(CustomerAccount customer, decimal amount)
        {
            var COT = _configContext.GetCurrentConfig().COT;
            var cotValue = CalculateCOT(COT, amount);
            var incomeGlId = _configContext.GetCurrentConfig().COTIncomeGlId;
            DebitCustomer(customer, cotValue);
            CreditIncomeGl( incomeGlId, cotValue);

            //update Log
            var COTPost = new COTPost
            {
                AccountToCreditId = incomeGlId,
                AccountToDebitId = customer.Id,
                Amount = cotValue,
                PostedAt =  DateTime.Now,
                //TellerId =  
            };
            _COTPostContext.Add(COTPost);
            _COTPostContext.Save(COTPost);

        }
        private string IsCustomerDebitable(CustomerAccount customer, decimal amount)
        {
            var cot = _configContext.GetCurrentConfig().COT;
            var cotValue = CalculateCOT(cot, amount);
            var customerAccountNumber = customer.AccountNumber;
            var result = "";
            if (customerAccountNumber.StartsWith("1"))//savings account
            {
                //Enforce minimum balance 
                result = customer.Balance >= amount+customer.Lien ? "Success" : $"Insufficient Funds In {customer.AccountName} {customer.AccountType} Account";
            }
            else if (customerAccountNumber.StartsWith("2"))//current account
            {
                //for current account enforce cot and minimum balance
                result = customer.Balance >= amount + customer.Lien + cotValue
                    ? "Success"
                    : $"Insufficient Funds In {customer.AccountName} {customer.AccountType} Account";
            }
            else//Loan Account
            {

            }

            return result;
        }
        private string IsGlCreditable(long tillAccountCode, decimal amount)
        {
            var result = "";
            var glAccount = _glAccountContext.GetByAccCode(tillAccountCode);
            result = glAccount.Balance >= amount ? "Success" : $"Insufficient Funds In {glAccount.Name}";
            return result;
        }
        public string PostEntry(TellerPost tellerPost)
        {
           
            var customer = _customerAccountContext.Get(tellerPost.CustomerAccountId);
            var amount = tellerPost.Amount;
            var tillAccount = _tellerContext.GetTillAccount(tellerPost.TellerId);
            var result = "";

            if (tellerPost.PostType == PostType.Deposit)        //for deposits
            {
               DebitGl(tillAccount, amount);
               CreditCustomer(customer, amount);
               result = "Success";
            }
            else if(tellerPost.PostType == PostType.Withdrawal) //for withdrawals
            {
                //for savings account and current accounts
                if (customer.AccountType != AccountType.Loan)
                {
                    var debitFeedback = IsCustomerDebitable(customer, amount); //check if customer account can be debited
                    if (debitFeedback == "Success")
                    {
                        var creditFeedback = IsGlCreditable(tillAccount, amount); // check if till is creditable 
                        if (creditFeedback == "Success")
                        {
                            if (customer.AccountType == AccountType.Current)
                            {
                                RemoveCOT(customer, amount);
                            }
                            DebitCustomer(customer, amount);
                            CreditGl(tillAccount, amount);
                          
                            result = creditFeedback;
                        }
                        else
                        {
                            //insufficient funds in till account
                            result = creditFeedback;
                        }
                    }
                    else
                    {
                        //Insufficient Funds in Customers Account
                        result = debitFeedback;
                    }
                }

                //for loans
                else
                {
                    result = "";

                }
            }
            else
            {
                result = "Post Type Not Specified (Deposit or Withdrawal)";
            }
            
            return result;
        }

        public string BuyCash(decimal amount, int tellerId)
        {
            var result = "";
            var tillAccount = _tellerContext.GetTillAccount(tellerId);
            var vaultAccCode = _glAccountContext.GetVaultAccCode();
            if (vaultAccCode != 0)  // check if vault exist
            {
                var creditFeedback = IsGlCreditable(vaultAccCode, amount);


                if (creditFeedback == "Success")
                {
                    CreditGl(vaultAccCode, amount);
                    DebitGl(tillAccount, amount);
                    result = creditFeedback;
                }
                else
                {
                    //insufficient funds in vault
                    result = creditFeedback;
                }
            }
            else
            {
                result = "Vault Doesn't Exist";
            }

            return result;
        }
        public string SellCash(decimal amount, int tellerId)
        {
            var result = "";
            var tillAccount = _tellerContext.GetTillAccount(tellerId);
            var vaultAccCode = _glAccountContext.GetVaultAccCode();
            if (vaultAccCode != 0) // check if vault exist
            {
                var creditFeedback = IsGlCreditable(tillAccount, amount);
                if (creditFeedback == "Success")
                {
                    CreditGl(tillAccount, amount);
                    DebitGl(vaultAccCode, amount);
                    result = creditFeedback;
                }
                else
                {
                    //insufficient funds in vault
                    result = creditFeedback;
                }
            }
            else
            {
                result = "Vault Doesn't Exist";
            }

            return result;
        }

        public decimal GetByTillBalance(int tellerId)
        {

            var glId = _tellerContext.GetTillAccount(Convert.ToInt32(tellerId));
            var result = _glAccountContext.GetByAccCode(glId).Balance;
            return result;


        }
    }
}
