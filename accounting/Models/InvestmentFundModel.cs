using System.Threading.Tasks;
using accounting.DbContexts;
using accounting.Exceptions;
using accounting.Services;
using accounting.ViewModels;

namespace accounting.Models
{
    public class InvestmentFundModel
    {
        private readonly DataBaseAddPeople _dataBaseAddPeopleService;
        private readonly DataBaseCheckNationalIdExist _dataBaseCheckNationalIdExistService;
        private readonly DataBaseCreateAccount _dataBaseCreateAccountService;
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;

        public InvestmentFundModel(string name, InvestmentFundDbContextFactory investmentFundDbContextFactory,
            DataBaseAddPeople addPeopleService, DataBaseCreateAccount createAccountService,
            DataBaseCheckNationalIdExist checkNationalIdExistService)
        {
            Name = name;
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
            _dataBaseAddPeopleService = addPeopleService;
            _dataBaseCreateAccountService = createAccountService;
            _dataBaseCheckNationalIdExistService = checkNationalIdExistService;
        }

        public string Name { get; }
        public int TotalCapital { get; }

        public async Task AddPeople(CreateAccountViewModel createAccountViewModel)
        {
            var people = new PeoplesModel(createAccountViewModel.NationalId!, createAccountViewModel.Name!,
                createAccountViewModel.LastName!, createAccountViewModel.FatherName!,
                createAccountViewModel.PersonalAccountNumber!,
                _dataBaseCreateAccountService,
                null);
            if (await _dataBaseCheckNationalIdExistService.CheckNationalIdExist(people))
                throw new NationalIdExistException(people.NationalId);
            await _dataBaseAddPeopleService.AddPeople(people);

            await people.AddAccount(people);
        }
    }
}