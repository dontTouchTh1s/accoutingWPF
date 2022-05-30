using System.Globalization;
using System.Windows;
using accounting.DataBase.DbContexts;
using accounting.DataBase.Services;
using accounting.Models;
using accounting.Store;
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
        private readonly NavigationService _navigationStore;

        public App()
        {
            CultureInfo.CurrentCulture = new CultureInfo("fa-IR");
            _investmentFundDbContextFactory = new InvestmentFundDbContextFactory(ConnectionString);
            DTOConverter dtoConverterService = new();
            DataBaseInvestmentFundServices dataBaseInvestmentFundServices =
                new(_investmentFundDbContextFactory, dtoConverterService);
            _investmentFundModel = new InvestmentFundModel
            ("MASHYEKHI",
                _investmentFundDbContextFactory,
                dataBaseInvestmentFundServices
            );
            _navigationStore = new NavigationService(_investmentFundModel);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (var dbContext = _investmentFundDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }


            MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(_investmentFundModel, _navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}