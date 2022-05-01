using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace accounting
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _amount;
        private SqlConnect _sql;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Connect to data base when form loaded
        /// </summary>
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sql = new SqlConnect();
            await _sql.Open();

            var data = await _sql.Query("SELECT amount FROM fund");
            while (data.Read())
                _amount = data["amount"].ToString();
            await data.CloseAsync();

            TBamount.Text = _amount;
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

        private async void CreateAccount(object sender, RoutedEventArgs e)
        {
            var name = FgName.TextBox.Text;
            var lastName = FgLastName.TextBox.Text;
            var fatherName = FgFatherName.TextBox.Text;
            var nationalId = FgNationalId.TextBox.Text;
            var accountNumber = FgSelfAccountNumber.TextBox.Text;
            var accountId = "";

            var reader = await _sql.Query(
                "INSERT INTO people (national_id, name, last_name, father_name, self_account_number) VALUES (@0, @1, @2, @3, @4)",
                name, lastName, fatherName, nationalId, accountNumber);
            await reader.CloseAsync();

            reader = await _sql.Query("SELECT account_id FROM people WHERE national_id = @0", nationalId);
            while (reader.Read()) accountId = reader["account_id"].ToString();
            await reader.CloseAsync();

            var toDay = DateTime.Today.ToString();
            reader = await _sql.Query("INSERT INTO accounts VALUES (@0, @1, @2)", accountId, "0", toDay);
            await reader.CloseAsync();


            LblMessage.Content = "حساب با موفقیت ساخته شد";
            LblMessage.Visibility = Visibility.Visible;
            await Animation_Label();
        }

        private async Task Animation_Label()
        {
            await Task.Delay(3000);
            var animation = new DoubleAnimation(0, TimeSpan.FromSeconds(2));
            LblMessage.BeginAnimation(OpacityProperty, animation);
        }
    }
}