using accounting.DTOs;
using Microsoft.EntityFrameworkCore;

namespace accounting.DbContexts
{
    public class PeoplesDbContext : DbContext
    {
        public PeoplesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PeoplesDTO> Accounts { get; set; }
    }
}