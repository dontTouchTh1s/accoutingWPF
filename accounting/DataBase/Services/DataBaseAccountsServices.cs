using System;
using System.Threading.Tasks;
using System.Windows.Documents;
using accounting.DataBase.DbContexts;
using accounting.Exceptions;
using accounting.Models;

namespace accounting.DataBase.Services
{
    public class DataBaseAccountsServices
    {
        private readonly DataBaseTransactionsServices _dataBaseTransactionsServices;
        private readonly DTOConverter _dtoConverterService;
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;

        public DataBaseAccountsServices(InvestmentFundDbContextFactory investmentFundDbContextFactory,
            DTOConverter dtoConverterService)
        {
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
            _dtoConverterService = dtoConverterService;
            _dataBaseTransactionsServices =
                new DataBaseTransactionsServices(_investmentFundDbContextFactory, _dtoConverterService);
        }

        public async Task CreateAccount(AccountsModel account)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var accountDTO = _dtoConverterService.AccountModelToDTO(account);
            context.Accounts.Add(accountDTO);
            await context.SaveChangesAsync();
        }

        public async Task MakeTransaction(TransactionsModel transactionsModel)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var accountDTO = await context.Accounts.FindAsync(transactionsModel.FundAccountId);
            var transactionsDTO = _dtoConverterService.TransactionsToDTO(transactionsModel);
            
            // Check if available credit is not enough on withdraw, throw exception
            if (accountDTO.AvailableCredit < (ulong)transactionsModel.Amount && transactionsModel.Amount < 0)
                throw new NotEnoughAvailableCreditException(accountDTO.AvailableCredit);
            
            context.Transactions.Add(transactionsDTO);
            switch (transactionsModel.Amount)
            {
                case < 0:
                    accountDTO.Credit -= (ulong)transactionsDTO.Amount;
                    break;
                case > 0:
                    accountDTO.Credit += (ulong)transactionsDTO.Amount;
                    break;
            }

            accountDTO.AvailableCredit = accountDTO.Credit;

            await context.SaveChangesAsync();
        }
    }
}