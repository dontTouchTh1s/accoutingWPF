using System.Windows;
using accounting.DataBase.DbContexts;
using accounting.DataBase.Services;
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
            DTOConverter dtoConverterService = new();
            DataBaseAddPeople addPeopleService = new(_investmentFundDbContextFactory, dtoConverterService);
            DataBaseCreateAccount createAccountService = new(_investmentFundDbContextFactory, dtoConverterService);
            DataBaseCheckNationalIdExist checkNationalIdExistService = new(_investmentFundDbContextFactory);
            _investmentFundModel = new InvestmentFundModel
            ("MASHYEKHI",
                _investmentFundDbContextFactory,
                addPeopleService,
                createAccountService,
                checkNationalIdExistService);
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