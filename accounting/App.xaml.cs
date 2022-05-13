using System.Windows;
using accounting.DbContexts;
using accounting.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace accounting
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var options = new DbContextOptionsBuilder().UseSqlite("Data Source=accounting.db").Options;
            var dbContext = new AccountsDbContext(options);
            dbContext.Database.Migrate();

            MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}