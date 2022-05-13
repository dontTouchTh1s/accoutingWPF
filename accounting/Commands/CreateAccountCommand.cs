using System.ComponentModel;
using System.Threading.Tasks;
using accounting.Models;
using accounting.ViewModels;

namespace accounting.Commands
{
    public class CreateAccountCommand : BaseAsyncCommand
    {
        private readonly CreateAccountViewModel _account;
        private readonly InvestmentFundModel _investmentFundModel;

        public CreateAccountCommand(CreateAccountViewModel account, InvestmentFundModel investmentFundModel)
        {
            _account = account;
            _investmentFundModel = investmentFundModel;
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

        public override async Task ExecuteAsync()
        {
            var people = new PeoplesModel(_account.NationalId!, _account.Name!, _account.LastName!, _account.FatherName!,
                _account.PersonalAccountNumber!);
            await _investmentFundModel.AddPeople(people);
        }

    }
}