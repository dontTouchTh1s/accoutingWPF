using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using accounting.ViewModels;

namespace accounting
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string? _amount;
        private MsSql _sql = null!;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Connect to data base when form loaded
        /// </summary>
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sql = new MsSql();
            await _sql.Open();

            var data = await _sql.Query("SELECT amount FROM fund");
            while (data.Read())
                _amount = data["amount"].ToString();
            await data.CloseAsync();

            //BalanceTextBlock.Text = _amount;
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void textBox_Copy2_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private async Task Animation_Label()
        {
            //await Task.Delay(3000);
            //var animation = new DoubleAnimation(0, TimeSpan.FromSeconds(2));
            //LblMessage.BeginAnimation(OpacityProperty, animation);
        }
    }
}