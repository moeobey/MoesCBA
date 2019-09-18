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
   public class GlPostingLogic
    {
        private readonly GlPostingRepository _db = new GlPostingRepository(new ApplicationDbContext());
        private readonly GlAccountLogic _glAccountContext = new GlAccountLogic(); 
        public void Save(GlPost glPost)
        {
            _db.Add(glPost);
            _db.Save(glPost);
        }

        public IEnumerable<GlPost> GetAllPosts()
        {
            return _db.GetAllPosts();
        }

        public string GetAccountMainCategory(long accountCode)
        {
            var glAccountType = "";
            var account = accountCode.ToString();
            if (account.StartsWith("1"))
            {
                glAccountType = "Asset";
            }
            else if (account.StartsWith("2"))
            {
                glAccountType = "Liability";
            }
            else if (account.StartsWith("3"))
            {
                glAccountType = "Capital";
            }
            else if (account.StartsWith("4"))
            {
                glAccountType = "Income";
            }
            else if (account.StartsWith("5"))
            {
                glAccountType = "Expense";
            }
            else
            {
                glAccountType = "Unknown";
            }

            return glAccountType;
        }

        public string IsCreditable(long accountCode, decimal amount)
        {
            var glAccount =  _glAccountContext.GetByAccCode(accountCode);
            var result = "";
            if (GetAccountMainCategory(accountCode) == "Asset" || GetAccountMainCategory(accountCode) == "Expense")
            {
                result = glAccount.Balance >= amount ? "Success" : $"Insufficient Balance in {glAccount.Name} {GetAccountMainCategory(accountCode)} GL Account";
            }
            else
            {
                result = "Success";
            }
            return result;
        }
        public string IsDebitable(long accountCode, decimal amount)
        {
            var glAccount = _glAccountContext.GetByAccCode(accountCode);
            var result = "";
            if (GetAccountMainCategory(accountCode) == "Asset" || GetAccountMainCategory(accountCode) == "Expense")
            {
                result = "Success";
            }
            else
            {
                result = glAccount.Balance >= amount ? "Success" : $"Insufficient Balance in {glAccount.Name}  {GetAccountMainCategory(accountCode)} GL Account";
            }
            return result;

        }

        public void CreditGlAccount(long accountCode, decimal amount)
        {
            var gl = new GlAccount();
            var glAccount = _glAccountContext.GetByAccCode(accountCode);

            if (GetAccountMainCategory(accountCode) == "Asset" || GetAccountMainCategory(accountCode) == "Expense")
            {
                glAccount.Balance = glAccount.Balance - amount;
                _glAccountContext.Update(gl);
            }
            else
            {
                glAccount.Balance = glAccount.Balance + amount;
                _glAccountContext.Update(gl);
            }
        }

        public void DebitGlAccount(long accountCode, decimal amount)
        {
            var gl = new GlAccount();
            var glAccount = _glAccountContext.GetByAccCode(accountCode);
            
            if (GetAccountMainCategory(accountCode) == "Asset" || GetAccountMainCategory(accountCode) == "Expense")
            {
                glAccount.Balance = glAccount.Balance + amount;
                _glAccountContext.Update(gl);
            }
            else
            {
                glAccount.Balance = glAccount.Balance - amount;
                _glAccountContext.Update(gl);
            }
            
        }

        public string PostEntry(GlPost glPost)
        {
            string result = "";

            var accountToCredit =   _glAccountContext.GetAccCode(Convert.ToInt32(glPost.GlAccountToCreditId));
          var accountToDebit =   _glAccountContext.GetAccCode(Convert.ToInt32(glPost.GlAccountToDebitId));

            if (GetAccountMainCategory(accountToCredit) == "Asset" || GetAccountMainCategory(accountToCredit) == "Expense") //special case for credit gl accounts do credit then debit
            {
                var creditFeedback = IsCreditable(accountToCredit, glPost.Amount);     
                if (creditFeedback == "Success")
                {
                    var debitFeedback = IsDebitable(accountToDebit, glPost.Amount);
                    if (debitFeedback == "Success")
                    {//do the update
                        CreditGlAccount(accountToCredit, glPost.Amount);
                        DebitGlAccount(accountToDebit, glPost.Amount);
                        result = debitFeedback;

                    }
                    else
                    {                                                                           //return the feedback from trying to debit gl account
                        result = debitFeedback;
                    }
                }
                //credit failed return the reason why it failed
                else
                {
                    result = creditFeedback;                                                    //return error for not crediting gl

                }
            }
            //if it is not the special case perform debit then credit 
            else
            {
                var debitFeedback = IsDebitable(accountToDebit, glPost.Amount);

                if (debitFeedback == "Success")
                {
                    var creditFeedback = IsCreditable(accountToCredit, glPost.Amount);
                    if (creditFeedback == "Success")
                    {
                        CreditGlAccount(accountToCredit, glPost.Amount);
                        DebitGlAccount(accountToDebit, glPost.Amount);
                        result = creditFeedback;
                    }
                    else
                    {
                        result = creditFeedback;
                    }
                }
                else
                {
                    result = debitFeedback;
                }
            }
            return result;
        }
    }
}
