using System.ComponentModel;
using accounting.ViewModels;

namespace accounting.Commands
{
    public class CreateAccountCommand : BaseCommand
    {
        private readonly CreateAccountViewModel _account;

        public CreateAccountCommand(CreateAccountViewModel account)
        {
            _account = account;
            account.ErrorsChanged += CreateAccountViewModelPropertyChanged;
        }

        private void CreateAccountViewModelPropertyChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            if (_account.Name == null && _account.LastName == null) return false;

            return !_account.HasErrors;
        }

        public override void Execute(object? parameter)
        {
            /*var name = FgName.TextBox.Text;
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
            await Animation_Label();*/
        }
    }
}