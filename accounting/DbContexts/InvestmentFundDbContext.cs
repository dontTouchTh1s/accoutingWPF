using accounting.DTOs;
using Microsoft.EntityFrameworkCore;

namespace accounting.DbContexts
{
    public class InvestmentFundDbContext : DbContext
    {
        public InvestmentFundDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PeopleDTO> Peoples { get; set; }
        public DbSet<AccountDTO> Accounts { get; set; }
    }
}