using System.Windows;
using accounting.DbContexts;
using accounting.Models;
using accounting.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace accounting
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string ConnectionString = "Data Source=InvestmentFund.db";
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;
        private readonly InvestmentFundModel _investmentFundModel;

        public App()
        {
            _investmentFundDbContextFactory = new InvestmentFundDbContextFactory(ConnectionString);
            _investmentFundModel = new InvestmentFundModel("mashayekhi", _investmentFundDbContextFactory);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (var dbContext = _investmentFundDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(_investmentFundModel)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}