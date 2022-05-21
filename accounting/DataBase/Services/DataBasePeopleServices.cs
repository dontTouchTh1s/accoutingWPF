using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using accounting.DataBase.DbContexts;
using accounting.DataBase.DTOs;
using accounting.Exceptions;
using accounting.Models;

namespace accounting.DataBase.Services
{
    public class DataBasePeopleServices
    {
        private readonly DataBaseCheckNationalIdExist _dataBaseCheckNationalIdExistServices;
        private readonly DTOConverter _dtoConverterService;
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;

        public DataBasePeopleServices(InvestmentFundDbContextFactory investmentFundDbContextFactory,
            DTOConverter dtoConverterService)
        {
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
            _dataBaseCheckNationalIdExistServices = new DataBaseCheckNationalIdExist(_investmentFundDbContextFactory);
            _dtoConverterService = dtoConverterService;
            DataBaseAccountsServices =
                new DataBaseAccountsServices(_investmentFundDbContextFactory, _dtoConverterService);
        }

        public DataBaseAccountsServices DataBaseAccountsServices { get; }

        public async Task AddPeople(PeoplesModel people)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            if (await _dataBaseCheckNationalIdExistServices.CheckNationalIdExist(people))
                throw new NationalIdExistException(people.NationalId);
            var peopleDTO = _dtoConverterService.PeopleModelToDTO(people);
            context.Peoples.Add(peopleDTO);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AccountsModel>> GetAllAccounts(string ownerNationalId)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            IEnumerable<AccountDTO> accountDTOList =
                context.Accounts.Where(account => account.OwnerNationalId == ownerNationalId);
            return accountDTOList.Select(accountDTO =>
                _dtoConverterService.AccountDTOToModel(accountDTO, DataBaseAccountsServices)).ToList();
        }
    }
}