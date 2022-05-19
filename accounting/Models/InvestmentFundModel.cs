using System.Threading.Tasks;
using accounting.DataBase.DbContexts;
using accounting.DataBase.Services;
using accounting.Exceptions;
using accounting.ViewModels;

namespace accounting.Models
{
    public class InvestmentFundModel
    {
        private readonly DataBaseAddPeople _dataBaseAddPeopleService;
        private readonly DataBaseCheckNationalIdExist _dataBaseCheckNationalIdExistService;
        private readonly DataBaseCreateAccount _dataBaseCreateAccountService;
        private readonly DataBaseInvestmentFundServices _dataBaseInvestmentFundServices;
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;


        public InvestmentFundModel(string name, InvestmentFundDbContextFactory investmentFundDbContextFactory,
            DataBaseAddPeople addPeopleService, DataBaseCreateAccount createAccountService,
            DataBaseCheckNationalIdExist checkNationalIdExistService,
            DataBaseInvestmentFundServices dataBaseInvestmentFundServices)
        {
            Name = name;
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
            _dataBaseAddPeopleService = addPeopleService;
            _dataBaseCreateAccountService = createAccountService;
            _dataBaseCheckNationalIdExistService = checkNationalIdExistService;
            _dataBaseInvestmentFundServices = dataBaseInvestmentFundServices;
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

        public async Task<int> GetBalance()
        {
            return await _dataBaseInvestmentFundServices.GetBalance();
        }
    }
}