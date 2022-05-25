using System.Collections.Generic;
using System.Threading.Tasks;
using accounting.DataBase.DbContexts;
using accounting.DataBase.Services;
using accounting.ViewModels;

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
            var loanModel = new LoanModel(lendLoanViewModel.FundAccountId ?? 0, lendLoanViewModel.Amount ?? 0,
                lendLoanViewModel.InstalmentCount ?? 0, lendLoanViewModel.PersonalAccountNumber);
            await _dataBaseInvestmentFundServices.LendLoad(loanModel);
        }

        public async Task<IEnumerable<LoanModel>> GetAccountLoans(ushort? fundAccountId)
        {
            return await _dataBasePeopleServices.DataBaseAccountsServices.GetLoans(fundAccountId);
        }
    }
}