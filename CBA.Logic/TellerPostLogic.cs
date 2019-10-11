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
        private  readonly  GlPostingLogic _glPostContext  = new GlPostingLogic();
        private  readonly  AccountTypeConfigLogic _configContext = new AccountTypeConfigLogic();
        private readonly COTPostRepository _COTPostContext = new COTPostRepository(new ApplicationDbContext());
        private readonly TransactionLogLogic _logContext = new TransactionLogLogic();

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
            _logContext.LogTransaction(glAccount, amount, TransactionType.Debit);//log transaction
            _glAccountContext.Update(gl);
        }
        private void CreditGl(long tillAccount, decimal amount)
        {
            var account = new GlAccount();
            var glAccount = _glAccountContext.GetByAccCode(tillAccount);
            glAccount.Balance = glAccount.Balance - amount;
            _logContext.LogTransaction(glAccount, amount, TransactionType.Credit);//log transaction
            _glAccountContext.Update(account);
        }
        private void CreditIncomeGl(int incomeGlId, decimal  COT )
        {
            var account = new GlAccount();
            var glAccount = _glAccountContext.Get(incomeGlId);
            glAccount.Balance = glAccount.Balance + COT;
            _logContext.LogTransaction(glAccount, COT, TransactionType.Credit);//log transaction
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
            _customerAccountContext.DebitCustomer(customer, cotValue);
            CreditIncomeGl( incomeGlId, cotValue);

            //log transaction
            var COTPost = new COTPost
            {
                AccountToCreditId = incomeGlId,
                AccountToDebitId = customer.Id,
                Amount = cotValue,
                PostedAt =  DateTime.Now,
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
            else //current account
            {
                //for current account enforce cot and minimum balance
                result = customer.Balance >= amount + customer.Lien + cotValue
                    ? "Success"
                    : $"Insufficient Funds In {customer.AccountName} {customer.AccountType} Account";
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
            var configReport = _configContext.IsAccountConfigurationSet();

            if (configReport != "Success")//check if account configurations are set
            {
                result = configReport;
                return result;
            }
            if (tellerPost.PostType == PostType.Deposit)//for deposits
            {
               DebitGl(tillAccount, amount);
               _customerAccountContext.CreditCustomer(customer, amount);
               result = "Success";
            }
            else //for withdrawals
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
                            _customerAccountContext.DebitCustomer(customer, amount);
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
                    result = "This is a Loan Account, you cannot perform transactions";
                }
            }
            return result;
        }

        public string BuyCash(decimal amount, int tellerId)
        {
            var result = "";
            var tillAccount = _tellerContext.GetTillAccount(tellerId);
            var glId = _glAccountContext.GetByAccCode(tillAccount).Id;
            var vault = _glAccountContext.GetVault();
            if (vault !=null)  // check if vault exist
            {
                var creditFeedback = IsGlCreditable(vault.AccountCode, amount);
                if (creditFeedback == "Success")
                {
                    CreditGl(vault.AccountCode, amount);
                    DebitGl(tillAccount, amount);
                    //log Transaction in Gl Post Log
                     var glPost  = new GlPost
                     {
                         GlAccountToCreditId = vault.Id,
                         GlAccountToDebitId = glId,
                         Amount = amount ,
                         Narration = "Buy Cash",
                         UserId = tellerId,
                         PostedAt = DateTime.Now
                     };
                    _glPostContext.Save(glPost);
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
            var glId = _glAccountContext.GetByAccCode(tillAccount).Id;
            var vault = _glAccountContext.GetVault();
            if (vault != null) // check if vault exist
            {
                var creditFeedback = IsGlCreditable(tillAccount, amount);
                if (creditFeedback == "Success")
                {
                    CreditGl(tillAccount, amount);
                    DebitGl(vault.AccountCode, amount);
                    //log Transaction in Gl Post Log
                    var glPost = new GlPost
                    {
                        GlAccountToCreditId = vault.Id,
                        GlAccountToDebitId = glId,
                        Amount = amount,
                        Narration = "Sell Cash",
                        UserId = tellerId,
                        PostedAt = DateTime.Now
                    };
                    _glPostContext.Save(glPost);
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

        public decimal GetTillBalance(int tellerId)
        {
            decimal result = 0;
            var glId = _tellerContext.GetTillAccount(Convert.ToInt32(tellerId));
            if (glId != 0)
            {
                 result = _glAccountContext.GetByAccCode(glId).Balance;
            }
            return result;
        }
    }
}
