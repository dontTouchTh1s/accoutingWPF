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
            if (accountsCredit != null && accountsCredit.Any())
                return accountsCredit.Aggregate((a, c) => a +
                                                          c);
            return 0;
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

        public async Task<AccountsModel> GetLoanAccount(ushort accountId)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var loanAccount = await context.Accounts.Where(ac => ac.AccountId == accountId)
                .FirstOrDefaultAsync();
            return _dtoConverterService.AccountDTOToModel(loanAccount, DataBasePeopleServices.DataBaseAccountsServices);
        }

        public async Task<ushort> GetAccountsCount()
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var accountsCount = Convert.ToUInt16(await context.Accounts.CountAsync());
            return accountsCount;
        }

        public async Task RemoveLoan(ushort loanId)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var loanDto = await context.Loans.Where(dto => dto.Id == loanId).FirstAsync();
            context.Loans.Remove(loanDto);
            await context.SaveChangesAsync();
        }

        /// <summary>
        ///     Add a loan to database loan table and reduce the amount of loan, from each account available credit.
        ///     <param name="loanModel">Model of loan</param>
        /// </summary>
        public async Task<ushort> LendLoad(LoanModel loanModel)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var loanDTO = _dtoConverterService.LoanModelToDTO(loanModel);
            await context.Loans.AddAsync(loanDTO);
            await context.SaveChangesAsync();

            return loanDTO.Id;
        }

        public async Task UpdateFinalLoanAmount(ushort loenId, ulong amount)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var loan = await context.Loans.Where(dto => dto.Id == loenId).FirstAsync();
            loan.Amount = amount;
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

        public async Task<ulong> GetLoanPayedAmount(ushort loanId)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            ulong payedAmount = 0;
            foreach (var loanInstallment in context.LoanInstallments)
                if (loanInstallment.LoanId == loanId)
                    payedAmount += loanInstallment.Amount;
            return payedAmount;
        }

        public async Task<ulong> GetLoanAmount(ushort loanId)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            return context.Loans.First(dto => dto.Id == loanId).Amount;
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

        public async Task<LoanModel> GetLoan(ushort loanId)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var loanDto = await context.Loans.Where(dto => dto.Id == loanId).FirstAsync();
            return _dtoConverterService.LoanDTOToModel(loanDto);
        }

        public async Task<IEnumerable<LoanTransactinosModel>> GetLoanTransactions(ushort loanId)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var loanTransactionsList = await context.LoanTransactinos.Where(dto => dto.LoanId == loanId &&
                dto.Amount < 0).ToListAsync();
            return loanTransactionsList.Select(loanTransaction =>
                _dtoConverterService.LoanTransactionsDTOToModel(loanTransaction)).ToList();
        }

        public async Task PayLoanInstalment(InstalmentLoanModel instalmentLoanModel, InvestmentFundDbContext context)
        {
            var instalmentLoanDto = _dtoConverterService.LoanInstalmentModelToDTO(instalmentLoanModel);
            await context.LoanInstallments.AddAsync(instalmentLoanDto);
            await context.SaveChangesAsync();
        }

        public async Task MakeLoanTransaction(LoanTransactinosModel loanTransactinosModel,
            InvestmentFundDbContext context)
        {
            var accountDTO = await context.Accounts.FindAsync(loanTransactinosModel.AccountId);
            var loanTransactinosDTO = _dtoConverterService.LoanTransactionModelToDTO(loanTransactinosModel);
            accountDTO.AvailableCredit += (ulong)loanTransactinosModel.Amount;
            await context.LoanTransactinos.AddAsync(loanTransactinosDTO);
        }
    }
}