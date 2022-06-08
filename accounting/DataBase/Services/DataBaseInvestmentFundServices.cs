using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using accounting.DataBase.DbContexts;
using accounting.Exceptions;
using accounting.Models;
using Microsoft.EntityFrameworkCore;

namespace accounting.DataBase.Services
{
    public class DataBaseInvestmentFundServices
    {
        private readonly DTOConverter _dtoConverterService;
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;

        public DataBaseInvestmentFundServices(InvestmentFundDbContextFactory investmentFundDbContextFactory,
            DTOConverter dtoConverterService)
        {
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
            _dtoConverterService = dtoConverterService;
            DataBasePeopleServices = new DataBasePeopleServices(_investmentFundDbContextFactory, _dtoConverterService);
        }

        public DataBasePeopleServices DataBasePeopleServices { get; set; }

        public async Task<ulong> GetBalance()
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var accountsCredit = await context.Accounts.Select(r => r.Credit).ToListAsync();
            if (accountsCredit != null && accountsCredit.Any())
                return accountsCredit.Aggregate((a, c) => a + c);
            return 0;
        }

        public async Task<ulong> GetAvailableBalance()
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var accountsCredit = await context.Accounts.Select(r => r.AvailableCredit).ToListAsync();
            return accountsCredit.Aggregate((a, c) => a + c);
        }

        public async Task<IEnumerable<PeoplesModel>> GetAllPeoples()
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            return await context.Peoples
                .Select(peoplesDTO => _dtoConverterService.PeopleDTOToModel(peoplesDTO, DataBasePeopleServices))
                .ToListAsync();
        }

        public async Task<Dictionary<PeoplesModel, IEnumerable<AccountsModel>>> GetAllPeoplesAccounts()
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var peoples = await GetAllPeoples();
            var accountModelDictionary = new Dictionary<PeoplesModel, IEnumerable<AccountsModel>>();
            foreach (var people in peoples)
                accountModelDictionary.Add(people, await people.GetAllAccounts());
            return accountModelDictionary;
        }

        public async Task<IEnumerable<AccountsModel>> GetAllAccounts()
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();

            return context.Accounts.ToList().Select(accountDTO =>
                    _dtoConverterService.AccountDTOToModel(accountDTO, DataBasePeopleServices.DataBaseAccountsServices))
                .ToList();
        }

        public async Task<IEnumerable<PeoplesModel>?> FindPeople(string owner)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var query = from people in context.Peoples
                where (people.Name + " " + people.LastName).Contains(owner)
                select people;
            return await query.Select(dto => _dtoConverterService.PeopleDTOToModel(dto, DataBasePeopleServices))
                .ToListAsync();
        }

        public async Task<Dictionary<PeoplesModel, IEnumerable<AccountsModel>>> FindPeoplesAccounts(string owner)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();


            var peoplesAccounts = new Dictionary<PeoplesModel, IEnumerable<AccountsModel>>();
            var peoples = await FindPeople(owner);

            if (peoples == null) return peoplesAccounts;
            foreach (var people in peoples)
                peoplesAccounts.Add(people, await people.GetAllAccounts());
            return peoplesAccounts;
        }

        /// <summary>
        ///     Add a loan to database loan table and reduce the amount of loan, from each account available credit.
        ///     <param name="loanModel">Model of loan</param>
        /// </summary>
        public async Task LendLoad(LoanModel loanModel)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var fundAvailableBalance = Convert.ToDouble(await GetAvailableBalance());
            // If fund didnt have enough available credit to lend the loan, throw
            if (fundAvailableBalance < loanModel.Amount)
                throw new NotEnoughFundAvailableBalance(Convert.ToInt64(fundAvailableBalance));
            var loanAmount = Convert.ToDouble(loanModel.Amount);
            var loanAccount = await context.Accounts.Where(ac => ac.AccountId == loanModel.AccountId)
                .FirstOrDefaultAsync();
            // Amount of the loan can be twice of account credit. If it's not, throw
            if (loanAmount > loanAccount.Credit * 2)
                throw new NotEnoughCreditException(Convert.ToUInt64(loanAccount.Credit));
            // First, reduce amount of loan from each account available credit
            // We can reduce up to 500000 (Minimum credit)
            var accountsCount = await context.Accounts.CountAsync();
            var amountPerAccount = Convert.ToInt32(loanAmount / accountsCount);
            var minimumAmountPerAccount = amountPerAccount;
            if (amountPerAccount > InvestmentFundModel.MinimumCredit)
                minimumAmountPerAccount = InvestmentFundModel.MinimumCredit;
            foreach (var account in context.Accounts)
                account.AvailableCredit -= Convert.ToUInt64(minimumAmountPerAccount);
            // If any amount of loan remains, reduce from accounts that have available credit
            var remainAmount = Convert.ToInt32(loanAmount - minimumAmountPerAccount * accountsCount);
            if (remainAmount != 0)
            {
                var amountPercentPerAccount = remainAmount * 100 / fundAvailableBalance;
                foreach (var account in context.Accounts)
                {
                    var availableCredit = Convert.ToDouble(account.AvailableCredit);
                    if (availableCredit == 0) break;
                    var loanAmountForAccount = Convert.ToInt32(availableCredit / 100 * amountPercentPerAccount);
                    account.AvailableCredit -= Convert.ToUInt64(loanAmountForAccount);
                }
            }

            var loanDTO = _dtoConverterService.LoanModelToDTO(loanModel);
            context.Loans.Add(loanDTO);
            await context.SaveChangesAsync();
        }

        public async Task<Dictionary<PeoplesModel, Dictionary<AccountsModel, List<LoanModel>>>> GetAllLoans()
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var loansByPeopleAccounts = new Dictionary<PeoplesModel, Dictionary<AccountsModel, List<LoanModel>>>();
            foreach (var people in context.Peoples)
            {
                var accountDic = new Dictionary<AccountsModel, List<LoanModel>>();
                foreach (var account in context.Accounts)
                {
                    if (account.Owner != people) continue;
                    var loanList = new List<LoanModel>();
                    foreach (var loan in context.Loans)
                    {
                        if (loan.Account != account) continue;
                        loanList.Add(_dtoConverterService.LoanDTOToModel(loan));
                    }

                    accountDic.Add(
                        _dtoConverterService.AccountDTOToModel(account,
                            DataBasePeopleServices.DataBaseAccountsServices), loanList);
                }

                loansByPeopleAccounts.Add(_dtoConverterService.PeopleDTOToModel(people, DataBasePeopleServices),
                    accountDic);
            }

            return loansByPeopleAccounts;
        }

        public async Task<ulong> GetLoanPayedAmount(LoanModel loan)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            ulong payedAmount = 0;
            foreach (var loanInstallment in context.LoanInstallments)
                if (loanInstallment.LoanId == loan.Id)
                    payedAmount += loanInstallment.Amount;
            return payedAmount;
        }

        public async Task<Dictionary<PeoplesModel, Dictionary<AccountsModel, List<TransactionsModel>>>>
            GetAllTransactions()
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var loansByPeopleAccounts =
                new Dictionary<PeoplesModel, Dictionary<AccountsModel, List<TransactionsModel>>>();
            foreach (var people in context.Peoples)
            {
                var accountDic = new Dictionary<AccountsModel, List<TransactionsModel>>();
                foreach (var account in context.Accounts)
                {
                    if (account.Owner != people) continue;
                    var transactionsList = new List<TransactionsModel>();
                    foreach (var transaction in context.Transactions)
                    {
                        if (transaction.Account != account) continue;
                        transactionsList.Add(_dtoConverterService.TransactionDTOToModel(transaction));
                    }

                    accountDic.Add(
                        _dtoConverterService.AccountDTOToModel(account,
                            DataBasePeopleServices.DataBaseAccountsServices), transactionsList);
                }

                loansByPeopleAccounts.Add(_dtoConverterService.PeopleDTOToModel(people, DataBasePeopleServices),
                    accountDic);
            }

            return loansByPeopleAccounts;
        }
    }
}