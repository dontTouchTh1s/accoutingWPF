using accounting.DTOs;
using Microsoft.EntityFrameworkCore;

namespace accounting.DbContexts
{
    public class AccountsDbContext : DbContext
    {
        public AccountsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AccountsDTO> Accounts { get; set; }
    }
}