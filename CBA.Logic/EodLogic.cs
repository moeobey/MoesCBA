using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBA.Core.Implementation;
using CBA.Data;
using CBA.Data.Implementation;
using CBA.Data.Migrations;

namespace CBA.Logic
{
  public  class EodLogic
    {
       
        private readonly AccountTypeConfigLogic _configContext = new AccountTypeConfigLogic();
        private readonly CustomerAccountLogic _customerAccContext = new CustomerAccountLogic();
        private readonly LoanLogic _loanAccContext = new LoanLogic();
        private readonly GlAccountLogic _glAccContext = new GlAccountLogic();
        private readonly GlPostingLogic _glPostContext = new GlPostingLogic();
        private readonly BankConfigLogic _bankConfigContext = new BankConfigLogic();



        public String RunEod()
        {
            var result = "";
            // confirm account type configurations are set 
            var configReport = _configContext.IsAccountConfigurationSet();
            if (configReport == "Success")
            {
                SaveLoanInterestAccrued();
                SaveCustomerInterestAccrued();
                _bankConfigContext.IncreaseFinancialDate();
                result = "Success";
            }
            else
            {
                //specify which configuration is incomplete
                result = configReport;
            }
            return result;

        }

        public void SaveLoanInterestAccrued()
        {
            var loanGl = _glAccContext.Get(_configContext.GetLoanConfig().InterestIncomeGlId);
            var loanAcc = _loanAccContext.GetAllLoans();
            if (loanAcc != null)
            {
                foreach (var account in loanAcc)
                {
                    var customerLoanAccount = _customerAccContext.GetByAccountNumber(account.AccountNumber);
                    //compute interest
                    _customerAccContext.DebitCustomer(account.CustomerAccount, account.InterestPayable);
                    _glPostContext.CreditGlAccount(loanGl, account.InterestPayable);
                    //log transaction at EodLog table ...


                    //reduce loan interest remaining
                    account.InterestRemaining -= account.InterestPayable;

                    

                    //compute loan repayment
                    _customerAccContext.DebitCustomer(account.CustomerAccount, account.LoanPayable);
                    _customerAccContext.CreditCustomer(customerLoanAccount, account.LoanPayable);

                    //log transaction at EodLog table ...
                   

                    //reducing loan amount remaining
                    account.LoanAmountRemaining -= account.LoanPayable;

                    //increase day count
                    account.DayCount++;
                    _loanAccContext.Update(account);
                }
            }
        }
        public void SaveCustomerInterestAccrued()
        {
            int[] daysInMonth = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            var financialDate = _bankConfigContext.GetConfig().FinancialDate;
            var presentDay = financialDate.Day;
            var presentMonth = financialDate.Month;
            //Financial Date Has not been Increased!!!
            var daysLeftInMonth = (daysInMonth[presentMonth - 1] - presentDay) + 1;
            var customerAccounts = _customerAccContext.GetAllExceptLoan();
            var savingsConfig = _configContext.GetSavingsConfig();
            var currentConfig = _configContext.GetCurrentConfig();
            if (customerAccounts != null)
            {
                foreach (var customerAccount in customerAccounts)
                {
                    decimal monthlyInterestRemaining = 0;
                    decimal interestPayable = 0;

                    if (customerAccount.AccountType == AccountType.Current) //for current accounts
                    {
                        if (customerAccount.Balance >= currentConfig.MinBalance)//check if customer is eligible for receiving interest
                        {
                            monthlyInterestRemaining = customerAccount.Balance * (currentConfig.CInterestRate / 100) *
                                                       (daysLeftInMonth / (decimal) daysInMonth[presentMonth - 1]);
                            interestPayable = monthlyInterestRemaining / daysLeftInMonth;
                            //increment customer Interest accrued and disburse if its month end
                            customerAccount.Interest += interestPayable;

                            //credit the interest payable gl (it will be recorded at the of the month) and debit interest expense account
                            _glPostContext.DebitGlAccount(currentConfig.InterestExpenseGl, interestPayable);
                            _glPostContext.CreditGlAccount(currentConfig.InterestPayableGl, interestPayable);
                            //log transaction...

                        }
                    }
                    else
                    {
                        if (customerAccount.Balance >= savingsConfig.MinBalance)
                        {
                            //for Savings account
                            monthlyInterestRemaining = customerAccount.Balance * (savingsConfig.CInterestRate / 100) *
                                                       (daysLeftInMonth / (decimal)daysInMonth[presentMonth - 1]);
                            interestPayable = monthlyInterestRemaining / daysLeftInMonth;
                            //increment customer Interest accrued and disburse if its month end
                            customerAccount.Interest += interestPayable;

                            //credit the interest payable gl for record purpose and debit interest expense account
                            _glPostContext.DebitGlAccount(savingsConfig.InterestExpenseGl, interestPayable);
                            _glPostContext.CreditGlAccount(savingsConfig.InterestPayableGl, interestPayable);

                            //log transaction...
                        }
                    }
                    _customerAccContext.Update(customerAccount);
                }

                if (presentDay == daysInMonth[presentMonth - 1]) //end of month, Disburse customers Accrued Interest
                {
                    foreach (var customerAccount in customerAccounts)
                    {
                        _customerAccContext.CreditCustomer(customerAccount, customerAccount.Interest);
                        if (customerAccount.AccountType == AccountType.Current)
                        {
                            _glPostContext.DebitGlAccount(currentConfig.InterestPayableGl, customerAccount.Interest);
                        }
                        else
                        {
                            _glPostContext.DebitGlAccount(savingsConfig.InterestPayableGl, customerAccount.Interest);
                        }
                        //reset customers interest accrued;
                        customerAccount.Interest = 0;

                        //log transaction ...
                        _customerAccContext.Update(customerAccount);
                    }
                }
            }

        }
    }
}
