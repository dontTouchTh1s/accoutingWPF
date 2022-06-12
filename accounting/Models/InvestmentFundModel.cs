using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using accounting.DataBase.DbContexts;
using accounting.DataBase.Services;
using accounting.Exceptions;
using accounting.ViewModels.ManageAccounts;
using accounting.ViewModels.ManageLoans;
using accounting.ViewModels.ManageTranactions;

namespace accounting.Models
{
    public class InvestmentFundModel
    {
        public const int MinimumCredit = 500000;
        private readonly DataBaseInvestmentFundServices _dataBaseInvestmentFundServices;
        private readonly DataBasePeopleServices _dataBasePeopleServices;
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;


        public InvestmentFundModel(string name, InvestmentFundDbContextFactory investmentFundDbContextFactory,
            DataBaseInvestmentFundServices dataBaseInvestmentFundServices)
        {
            Name = name;
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
            _dataBaseInvestmentFundServices = dataBaseInvestmentFundServices;
            _dataBasePeopleServices = _dataBaseInvestmentFundServices.DataBasePeopleServices;
        }

        private string Name { get; }

        public async Task AddPeople(CreateAccountViewModel createAccountViewModel)
        {
            var people = new PeoplesModel(createAccountViewModel.NationalId!, createAccountViewModel.Name!,
                createAccountViewModel.LastName!, createAccountViewModel.FatherName!,
                createAccountViewModel.PersonalAccountNumber!,
                _dataBasePeopleServices);

            await _dataBasePeopleServices.AddPeople(people);

            await people.AddAccount(people, createAccountViewModel.Credit);
        }

        public async Task<ulong> GetBalance()
        {
            return await _dataBaseInvestmentFundServices.GetBalance();
        }

        public async Task<ulong> GetAvailableBalance()
        {
            return await _dataBaseInvestmentFundServices.GetAvailableBalance();
        }

        public async Task MakeTransaction(TransactionsViewModel transactionsViewModel)
        {
            var transactionModel = new TransactionsModel(transactionsViewModel.Amount ?? 0,
                transactionsViewModel.FundAccountId ?? 0, transactionsViewModel.PersonalAccountNumber);
            await _dataBasePeopleServices.DataBaseAccountsServices.MakeTransaction(transactionModel);
        }

        public async Task<IEnumerable<AccountsModel>> GetAllAccounts()
        {
            return await _dataBaseInvestmentFundServices.GetAllAccounts();
        }

        public async Task<Dictionary<PeoplesModel, IEnumerable<AccountsModel>>?> GetAllPeoplesAccounts()
        {
            return await _dataBaseInvestmentFundServices.GetAllPeoplesAccounts();
        }

        public async Task LendLoan(LendLoanViewModel lendLoanViewModel)
        {
            // Create Loan model with 0 amount, cuz its not final amount, final amount will caculate after all accounts
            // transactions done, then update loan amount
            var loanModel = new LoanModel(lendLoanViewModel.FundAccountId ?? 0, 0,
                lendLoanViewModel.InstalmentCount ?? 0, lendLoanViewModel.PersonalAccountNumber);
            var fundAvailableBalance = await GetAvailableBalance();
            // If fund didnt have enough available credit to lend the loan, throw
            if (fundAvailableBalance < lendLoanViewModel.Amount)
                throw new NotEnoughFundAvailableBalanceException(fundAvailableBalance);
            var loanAmount = Convert.ToDouble(lendLoanViewModel.Amount);
            // Amount of the loan can be twice of account credit. If it's not, throw
            var loanAccount = await _dataBaseInvestmentFundServices.GetLoanAccount(loanModel.AccountId);
            if (loanAmount > loanAccount.Credit * 2)
                throw new NotEnoughCreditException(Convert.ToUInt64(loanAccount.Credit));
            // Reduce amount of loan from each account available credit and make transaction of that
            var accountsCount = await _dataBaseInvestmentFundServices.GetAccountsCount();
            var amountPerAccount = Convert.ToUInt64(lendLoanViewModel.Amount / accountsCount);
            ushort reduced = 0;
            var accoutnsList = await _dataBaseInvestmentFundServices.GetAllAccounts();
            // In this section, two loan-related transactions are performed,
            // ie creating a withdrawal transaction in the accounts and creating a loan.
            // If there is an error in the transaction, none is done.
            // First Add loan, and get loan id, after that make trasactinos
            // And save dataBase.
            var loanId = await _dataBaseInvestmentFundServices.LendLoad(loanModel);
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            ulong finalLoanAmount = 0;
            try
            {
                foreach (var account in accoutnsList)
                {
                    // If an account does not have enough credit,
                    // we deduct all its credit and deduct the remaining loan amount from the other accounts.
                    if (account.AvailableCredit < amountPerAccount)
                    {
                        var maximumAmount = account.AvailableCredit;
                        var remainedAmount = amountPerAccount - maximumAmount;
                        var loanTransactinosModel = new LoanTransactinosModel(-Convert.ToInt64(maximumAmount),
                            loanId,
                            account.Id,
                            loanModel.PersonalAccountNumber);
                        await _dataBaseInvestmentFundServices.MakeLoanTransaction(loanTransactinosModel, context);
                        finalLoanAmount += maximumAmount;
                        amountPerAccount += remainedAmount / Convert.ToUInt64(accountsCount - reduced);
                    }
                    else
                    {
                        var loanTransactinosModel = new LoanTransactinosModel(-Convert.ToInt64(amountPerAccount),
                            loanId,
                            account.Id,
                            loanModel.PersonalAccountNumber);
                        await _dataBaseInvestmentFundServices.MakeLoanTransaction(loanTransactinosModel, context);
                        finalLoanAmount += amountPerAccount;
                    }

                    reduced++;
                }
            }
            catch
            {
                await _dataBaseInvestmentFundServices.RemoveLoan(loanId);
            }

            await _dataBaseInvestmentFundServices.UpdateFinalLoanAmount(loanId, finalLoanAmount);
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsInstalmentAmountValid(ulong instalmentAmount, ushort loanId)
        {
            var remainedAmount =
                await GetLoanRemainedAmount(loanId);
            return remainedAmount >= instalmentAmount;
        }

        public async Task<IEnumerable<LoanModel>> GetAccountLoans(ushort? fundAccountId)
        {
            return await _dataBasePeopleServices.DataBaseAccountsServices.GetAccountLoans(fundAccountId);
        }

        public async Task<Dictionary<PeoplesModel, Dictionary<AccountsModel, List<LoanModel>>>> GetAllLoans()
        {
            return await _dataBaseInvestmentFundServices.GetAllLoans();
        }

        public async Task<ulong> GetLoanPayedAmount(ushort loanId)
        {
            return await _dataBaseInvestmentFundServices.GetLoanPayedAmount(loanId);
        }

        public async Task<ulong> GetLoanRemainedAmount(ushort loanId)
        {
            var payedAmount = await GetLoanPayedAmount(loanId);
            return await _dataBaseInvestmentFundServices.GetLoanAmount(loanId) - payedAmount;
        }

        public async Task<Dictionary<PeoplesModel, Dictionary<AccountsModel, List<TransactionsModel>>>>
            GetAllTransactinos()
        {
            return await _dataBaseInvestmentFundServices.GetAllTransactions();
        }

        public async Task PayLoanInstalment(InstalmentLoanViewModel instalmentLoanViewModel)
        {
            if (instalmentLoanViewModel.PayFromFund)
            {
                var transactinoModel = new TransactionsModel(
                    Convert.ToInt64(instalmentLoanViewModel.Amount) * -1,
                    instalmentLoanViewModel.FundAccountId ?? 0,
                    instalmentLoanViewModel.PersonalAccountNumber
                );
                await _dataBasePeopleServices.DataBaseAccountsServices.MakeTransaction(transactinoModel);
            }

            var instalmentLoanModel = new InstalmentLoanModel(instalmentLoanViewModel.LoanId ?? 0,
                instalmentLoanViewModel.Amount
            );
            // Make loan transactions to add velue of payed instalment to accounts available credit
            var loanModel = await _dataBaseInvestmentFundServices.GetLoan(instalmentLoanModel.LoanId);
            var loanTransactions =
                await _dataBaseInvestmentFundServices.GetLoanTransactions(instalmentLoanModel.LoanId);
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            foreach (var loanTransaction in loanTransactions)
            {
                var accountLoanAmountPresentPayed =
                    Convert.ToInt64(Convert.ToUInt64(Math.Abs(loanTransaction.Amount * 100)) / loanModel.Amount);
                var amount = accountLoanAmountPresentPayed * Convert.ToInt64(instalmentLoanModel.Amount) / 100;
                var loanTransactinoModel = new LoanTransactinosModel(
                    loanTransaction.Id,
                    amount,
                    loanTransaction.Date,
                    loanTransaction.LoanId,
                    loanTransaction.AccountId,
                    loanTransaction.PersonalAccountNumber);
                await _dataBaseInvestmentFundServices.MakeLoanTransaction(loanTransactinoModel, context);
            }

            // Then add instalment
            await _dataBaseInvestmentFundServices.PayLoanInstalment(instalmentLoanModel, context);
        }

        public async Task<Dictionary<LoanModel, ulong>> GetAccountUnpaidLoans(ushort? fundAccountId)
        {
            return await _dataBasePeopleServices.DataBaseAccountsServices.GetAccountUnpaidLoans(fundAccountId);
        }
    }
}