using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace accounting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _amount;
        private readonly SqlConnect _sql;

        public MainWindow()
        {
            _sql = new();
            _sql.Open();
            SqlDataReader data = _sql.Query("SELECT amount FROM fund");
            while (data.Read())
            {
                _amount = data["amount"].ToString();
            }

            data.Close();

            InitializeComponent();
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

        private async void create_account(object sender, RoutedEventArgs e)
        {
            string name = TbName.Text;
            string lastName = TbLastName.Text;
            string fatherName = TbFatherName.Text;
            string nationalId = TbNationalId.Text;
            string accountNumber = TbAccountNumber.Text;
            string accountId = "";
            await _sql.Query(
                "INSERT INTO people (national_id, name, last_name, father_name, self_account_number) VALUES (@0, @1, @2, @3, @4)",
                name, lastName, fatherName, nationalId, accountNumber).CloseAsync();
            var reader = _sql.Query("SELECT account_id FROM people WHERE national_id = @0", nationalId);
            while (reader.Read())
            {
                accountId = reader["account_id"].ToString();
            }
            await reader.CloseAsync();

            var toDay = DateTime.Today.ToString();
            await _sql.Query("INSERT INTO accounts VALUES (@0, @1, @2)", accountId, "0", toDay).CloseAsync();


            LblMessage.Content = "حساب با موفقیت ساخته شد";
            LblMessage.Visibility = Visibility.Visible;
            await Animation_Label();
        }

        private async Task Animation_Label()
        {
            await Task.Delay(2000);
            DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(2));
            LblMessage.BeginAnimation(Label.OpacityProperty, animation);
        }
    }
}