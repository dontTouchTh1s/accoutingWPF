using System.Collections.Generic;
using System.Threading.Tasks;
using accounting.DataBase.DbContexts;
using accounting.DataBase.Services;
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
            var loanModel = new LoanModel(lendLoanViewModel.FundAccountId ?? 0, lendLoanViewModel.Amount ?? 0,
                lendLoanViewModel.InstalmentCount ?? 0, lendLoanViewModel.PersonalAccountNumber);
            await _dataBaseInvestmentFundServices.LendLoad(loanModel);
        }

        public async Task<IEnumerable<LoanModel>> GetAccountLoans(ushort? fundAccountId)
        {
            return await _dataBasePeopleServices.DataBaseAccountsServices.GetLoans(fundAccountId);
        }

        public async Task<Dictionary<PeoplesModel, Dictionary<AccountsModel, List<LoanModel>>>> GetAllLoans()
        {
            return await _dataBaseInvestmentFundServices.GetAllLoans();
        }

        public async Task<ulong> GetLoanPayedAmount(LoanModel loanModel)
        {
            return await _dataBaseInvestmentFundServices.GetLoanPayedAmount(loanModel);
        }

        public async Task<ulong> GetLoanRemainedAmount(LoanModel loanModel)
        {
            var payedAmount = await GetLoanPayedAmount(loanModel);
            return loanModel.Amount - payedAmount;
        }

        public async Task<Dictionary<PeoplesModel, Dictionary<AccountsModel, List<TransactionsModel>>>>
            GetAllTransactinos()
        {
            return await _dataBaseInvestmentFundServices.GetAllTransactions();
        }

        public async Task PayLoanInstalment(InstalmentLoanViewModel instalmentLoanViewModel)
        {
            var instalmentLoanModel = new InstalmentLoanModel(instalmentLoanViewModel.LoanId ?? 0,
                instalmentLoanViewModel.Amount
            );
            await _dataBaseInvestmentFundServices.PayLoanInstalment(instalmentLoanModel);
        }
    }
}