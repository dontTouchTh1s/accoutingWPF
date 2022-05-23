using System.Threading.Tasks;
using accounting.DataBase.DbContexts;
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
            context.Transactions.Add(transactionsDTO);
            accountDTO.Credit += transactionsDTO.Amount;
            accountDTO.AvailableCredit = accountDTO.Credit;

            await context.SaveChangesAsync();
        }
    }
}