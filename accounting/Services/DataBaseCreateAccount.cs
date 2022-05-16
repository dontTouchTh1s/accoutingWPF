using System.Threading.Tasks;
using accounting.DbContexts;
using accounting.Models;

namespace accounting.Services
{
    public class DataBaseCreateAccount
    {
        private readonly DTOConverter _dtoConverterService;
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;

        public DataBaseCreateAccount(InvestmentFundDbContextFactory investmentFundDbContextFactory,
            DTOConverter dtoConverterService)
        {
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
            _dtoConverterService = dtoConverterService;
        }

        public async Task CreateAccount(AccountsModel account)
        {
            await using (var context = _investmentFundDbContextFactory.CreateDbContext())
            {
                var accountDTO = _dtoConverterService.AccountModelToDTO(account);
                context.Accounts.Add(accountDTO);
                await context.SaveChangesAsync();
            }
        }
    }
}