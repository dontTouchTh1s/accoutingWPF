using System.Threading.Tasks;
using accounting.DataBase.DbContexts;
using accounting.DataBase.Services;
using accounting.ViewModels;

namespace accounting.Models
{
    public class InvestmentFundModel
    {
        private readonly DataBaseInvestmentFundServices _dataBaseInvestmentFundServices;
        private readonly DataBasePeopleServices _dataBasePeopleServices;
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;


        public InvestmentFundModel(string name, InvestmentFundDbContextFactory investmentFundDbContextFactory,
            DataBaseInvestmentFundServices dataBaseInvestmentFundServices,
            DataBasePeopleServices dataBasePeopleServices)
        {
            Name = name;
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
            _dataBaseInvestmentFundServices = dataBaseInvestmentFundServices;
            _dataBasePeopleServices = dataBasePeopleServices;
        }

        public string Name { get; }
        public int TotalCapital { get; }

        public async Task AddPeople(CreateAccountViewModel createAccountViewModel)
        {
            var people = new PeoplesModel(createAccountViewModel.NationalId!, createAccountViewModel.Name!,
                createAccountViewModel.LastName!, createAccountViewModel.FatherName!,
                createAccountViewModel.PersonalAccountNumber!,
                null,
                _dataBasePeopleServices);

            await _dataBasePeopleServices.AddPeople(people);

            await people.AddAccount(people);
        }

        public async Task<int> GetBalance()
        {
            return await _dataBaseInvestmentFundServices.GetBalance();
        }

        public async Task MakeTransaction(TransactionsViewModel transactionsViewModel)
        {

            var transactionModel = new TransactionsModel
            {
                FundAccountId = transactionsViewModel.FundAccountId,
                Amount = transactionsViewModel.Amount,
                PersonalAccountNumber = transactionsViewModel.PersonalAccountNumber
            };
            await MakeTransactions(transactionModel);
        }
        public async Task MakeTransactions(TransactionsModel transactionsModel)
        {
            await _dataBasePeopleServices.DataBaseAccountsServices.MakeTransaction(transactionsModel);
        }
    }
}