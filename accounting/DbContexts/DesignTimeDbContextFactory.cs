using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace accounting.DbContexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AccountsDbContext>
    {
        public AccountsDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder().UseSqlite("Data Source=accounting.db").Options;
            return new AccountsDbContext(options);
        }
    }
}