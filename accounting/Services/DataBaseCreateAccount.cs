using System.Threading.Tasks;
using accounting.DbContexts;
using accounting.Models;

namespace accounting.Services
{
    public class DataBaseCreateAccount
    {
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;

        public DataBaseCreateAccount(InvestmentFundDbContextFactory investmentFundDbContextFactory)
        {
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
        }

        public async Task CreateAccount(AccountsModel account)
        {
            await using (var context = _investmentFundDbContextFactory.CreateDbContext())
            {
                var dtoConverter = new DTOConverter();
                var accountDTO = dtoConverter.AccountModelToDTO(account);
                context.Accounts.Add(accountDTO);
                await context.SaveChangesAsync();
            }
        }
    }
}