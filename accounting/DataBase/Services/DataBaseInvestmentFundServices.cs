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

        public async Task<int> GetBalance()
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var accountsCredit = await context.Accounts.Select(r => r.Credit).ToListAsync();
            return accountsCredit.Sum();
        }
        public async Task<int> GetAvailableBalance()
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var accountsCredit = await context.Accounts.Select(r => r.AvailableCredit).ToListAsync();
            return accountsCredit.Sum();
        }

        public async Task<IEnumerable<PeoplesModel>> GetAllPeoples()
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            return context.Peoples
                .Select(peoplesDTO => _dtoConverterService.PeopleDTOToModel(peoplesDTO, DataBasePeopleServices))
                .ToList().AsEnumerable();
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

            foreach (var people in peoples) peoplesAccounts.Add(people, await people.GetAllAccounts());
            return peoplesAccounts;
        }
        /// <summary>
        ///     Add a loan to database loan table and reduce the amount of loan, from each account available credit.
        /// <param name="loanModel">Model of loan</param>
        /// </summary>        
        public async Task LendLoad(LoanModel loanModel)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var fundAvailableBalance = Convert.ToDouble(await GetAvailableBalance());
            // If fund didnt have enough available credit to lend the loan, throw
            if (fundAvailableBalance < loanModel.Amount)
                throw new NotEnoughFundAvailableBalance(Convert.ToInt64(fundAvailableBalance));
            var loanAmount = Convert.ToDouble(loanModel.Amount);
            var loanAccount = await context.Accounts.Where(ac => ac.AccountId == loanModel.AccountId).FirstOrDefaultAsync();
            // Amount of the loan can be twice of account credit. If it's not, throw
            if (loanAmount > loanAccount.Credit * 2)
                throw new NotEnoughCreditException(loanAccount.Credit);
            // First, reduce amount of loan from each account available credit
            // We can reduce up to 500000 (Minimum credit)
            var accountsCount = await context.Accounts.CountAsync();
            var amountPerAccount = Convert.ToInt32(loanAmount / accountsCount);
            var minimumAmountPerAccount = amountPerAccount;
            if (amountPerAccount > InvestmentFundModel.MinimumCredit)
                minimumAmountPerAccount =  InvestmentFundModel.MinimumCredit;
            foreach (var account in context.Accounts)
            {
                account.AvailableCredit -= minimumAmountPerAccount;
            }
            // If any amount of loan remains, reduce from accounts that have available credit
            var remainAmount = Convert.ToInt32(loanAmount - (minimumAmountPerAccount * accountsCount));
            if (remainAmount != 0)
            {
                var amountPercentPerAccount = remainAmount * 100 / fundAvailableBalance;
                foreach (var account in context.Accounts)
                {
                    var availableCredit = Convert.ToDouble(account.AvailableCredit);
                    if (availableCredit == 0) break;
                    var loanAmountForAccount = Convert.ToInt32(availableCredit / 100 * amountPercentPerAccount);
                    account.AvailableCredit -= loanAmountForAccount;
                }
            }

            var loanDTO = _dtoConverterService.LoanModelToDTO(loanModel);
            context.Loans.Add(loanDTO);
            await context.SaveChangesAsync();
        }

        
    }
}