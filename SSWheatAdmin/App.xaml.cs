using System.Globalization;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SSWheatAdmin.DataBase.DbContexts;
using SSWheatAdmin.DataBase.Services;
using SSWheatAdmin.Models;
using SSWheatAdmin.Store;
using SSWheatAdmin.ViewModels;
using SSWheatAdmin.ViewModels.ManageAccounts;
using SSWheatAdmin.ViewModels.ManageLoans;
using SSWheatAdmin.ViewModels.ManageTranactions;

namespace SSWheatAdmin
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
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (var dbContext = _investmentFundDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            var summeryViewModel = new SummeryViewModel(_investmentFundModel);
            var manageTransactionsViewModel = new ManageTransactionsViewModel(_investmentFundModel);
            var manageLoanViewModel = new ManageLoanViewModel(_investmentFundModel);
            var manageAccountsViewModel = new ManageAccountsViewModel(_investmentFundModel);
            var navigationStore = new NavigationService(summeryViewModel, manageTransactionsViewModel,
                manageLoanViewModel,
                manageAccountsViewModel);


            MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(_investmentFundModel, navigationStore),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Width = SystemParameters.PrimaryScreenWidth / 100 * 75,
                Height = SystemParameters.PrimaryScreenHeight / 100 * 75
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}