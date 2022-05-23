using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using accounting.DataBase.DbContexts;
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

        public async Task LendLoad(LoanModel loanModel)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var fundBalance = Convert.ToDouble(await GetBalance());
            var loanAmount = Convert.ToDouble(loanModel.Amount);
            if (loanModel.Amount > fundBalance)
                return;
            if (loanModel.Amount > fundBalance / 2)
                return;
            var amountPercentPerAccount = loanAmount * 100 / fundBalance;
            foreach (var account in context.Accounts)
            {
                var availableCredit = Convert.ToDouble(account.AvailableCredit);
                var loanAmountForAccount = Convert.ToInt32(availableCredit / 100 * amountPercentPerAccount);
                account.AvailableCredit -= loanAmountForAccount;
            }

            var loanDTO = _dtoConverterService.LoanModelToDTO(loanModel);
            context.Loans.Add(loanDTO);
            await context.SaveChangesAsync();
        }
    }
}