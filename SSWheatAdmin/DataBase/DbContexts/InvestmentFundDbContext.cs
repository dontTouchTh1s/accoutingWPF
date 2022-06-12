using Microsoft.EntityFrameworkCore;
using SSWheatAdmin.DataBase.DTOs;

namespace SSWheatAdmin.DataBase.DbContexts
{
    public class InvestmentFundDbContext : DbContext
    {
        public DbSet<PeoplesDTO> Peoples { get; set; }
        public DbSet<AccountDTO> Accounts { get; set; }
        public DbSet<LoansDTO> Loans { get; set; }
        public DbSet<TransactionsDTO> Transactions { get; set; }
        public DbSet<LoanInstallmentsDTO> LoanInstallments { get; set; }
        public DbSet<LoanTransactinosDTO> LoanTransactinos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=InvestmentFund.db");
        }
    }
}