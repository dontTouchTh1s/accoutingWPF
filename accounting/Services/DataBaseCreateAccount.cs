using System;
using System.Threading.Tasks;
using accounting.DbContexts;
using accounting.DTOs;

namespace accounting.Services
{
    public class DataBaseCreateAccount
    {
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;

        public DataBaseCreateAccount(InvestmentFundDbContextFactory investmentFundDbContextFactory)
        {
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
        }

        public async Task CreateAccount()
        {
            await using (var context = _investmentFundDbContextFactory.CreateDbContext())
            {
                var accountDTO = new AccountDTO
                {
                    CreateDate = DateTime.Now
                };
                context.Accounts.Add(accountDTO);
                await context.SaveChangesAsync();
            }
        }
    }
}