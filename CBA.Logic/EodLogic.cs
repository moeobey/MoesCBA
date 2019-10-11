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
            var loanAcc = _loanAccContext.GetAllUnpaidLoans();
            if (loanAcc != null)//check if there are unpaid loans
            {
                foreach (var account in loanAcc)
                {
                    var customerLoanAccount = _customerAccContext.GetByAccountNumber(account.AccountNumber);

                    if (account.DurationInMonths * 30 != account.DayCount) //check if loan duration is over
                    {
                        //compute interest
                        _customerAccContext.DebitCustomer(account.CustomerAccount, account.InterestPayable);
                        _glPostContext.CreditGlAccount(loanGl, account.InterestPayable);

                        //reduce loan interest remaining
                        account.InterestRemaining -= account.InterestPayable;

                        //compute loan repayment
                        _customerAccContext.DebitCustomer(account.CustomerAccount, account.LoanPayable);
                        _customerAccContext.CreditCustomer(customerLoanAccount, account.LoanPayable);

                        //reduce loan amount remaining
                        account.LoanAmountRemaining -= account.LoanPayable;
                    }

                    if (account.DurationInMonths * 30 == account.DayCount)//check if duration is complete and change status
                        account.IsLoanPaymentComplete = true;
                    
                    if (account.DurationInMonths * 30 != account.DayCount) //increase day count if loan is not repaid
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

                            if (daysLeftInMonth != 0)//to avoid divide by 0 error
                            {
                                interestPayable = monthlyInterestRemaining / daysLeftInMonth;
                            }

                            //increment customer Interest accrued
                            customerAccount.Interest += interestPayable;

                            //credit the interest payable gl (it will be recorded at the of the month) and debit interest expense account
                            _glPostContext.DebitGlAccount(currentConfig.InterestExpenseGl, interestPayable);
                            _glPostContext.CreditGlAccount(currentConfig.InterestPayableGl, interestPayable);
                        }
                    }
                    else
                    {
                        if (customerAccount.Balance >= savingsConfig.MinBalance)
                        {
                            //for Savings account
                            monthlyInterestRemaining = customerAccount.Balance * (savingsConfig.CInterestRate / 100) *
                                                       (daysLeftInMonth / (decimal)daysInMonth[presentMonth - 1]);

                            if (daysLeftInMonth != 0)//to avoid divide by 0 error
                            {
                                interestPayable = monthlyInterestRemaining / daysLeftInMonth;
                            }
                            //increment customer Interest accrued 
                            customerAccount.Interest += interestPayable;

                            //credit the interest payable gl for record purpose and debit interest expense account
                            _glPostContext.DebitGlAccount(savingsConfig.InterestExpenseGl, interestPayable);
                            _glPostContext.CreditGlAccount(savingsConfig.InterestPayableGl, interestPayable);
                        }
                    }
                    _customerAccContext.Update(customerAccount);
                }

                if (presentDay == daysInMonth[presentMonth - 1]) //end of month, Disburse customers Accrued Interest
                {
                    foreach (var customerAccount in customerAccounts)
                    {
                        _customerAccContext.CreditCustomer(customerAccount, customerAccount.Interest);
                        _glPostContext.DebitGlAccount(
                            customerAccount.AccountType == AccountType.Current
                                ? currentConfig.InterestPayableGl
                                : savingsConfig.InterestPayableGl, customerAccount.Interest);
                        //reset customers interest accrued;
                        customerAccount.Interest = 0;
                        _customerAccContext.Update(customerAccount);
                    }
                }
            }
        }
    }
}
