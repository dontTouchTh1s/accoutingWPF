using Microsoft.EntityFrameworkCore;

namespace SSWheatAdmin.DataBase.DbContexts
{
    public class InvestmentFundDbContextFactory
    {
        private readonly string _connectionString;

        public InvestmentFundDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public InvestmentFundDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;
            return new InvestmentFundDbContext();
        }
    }
}