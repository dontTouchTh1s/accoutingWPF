using accounting.DTOs;
using Microsoft.EntityFrameworkCore;

namespace accounting.DbContexts
{
    public class InvestmentFundDbContext : DbContext
    {
        public DbSet<PeopleDTO> Peoples { get; set; }
        public DbSet<AccountDTO> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=InvestmentFund.db");
        }
    }
}