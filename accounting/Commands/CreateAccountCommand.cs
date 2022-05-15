using System.ComponentModel;
using System.Threading.Tasks;
using accounting.Models;
using accounting.ViewModels;

namespace accounting.Commands
{
    public class CreateAccountCommand : BaseAsyncCommand
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private readonly CreateAccountViewModel _peopleViewModel;

        public CreateAccountCommand(CreateAccountViewModel account, InvestmentFundModel investmentFundModel)
        {
            _peopleViewModel = account;
            _investmentFundModel = investmentFundModel;
            account.ErrorsChanged += CreateAccountViewModelPropertyChanged;
        }

        private void CreateAccountViewModelPropertyChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            if (_peopleViewModel.Name == null && _peopleViewModel.LastName == null) return false;

            return !_peopleViewModel.HasErrors;
        }

        public override async Task ExecuteAsync()
        {
            await _investmentFundModel.AddPeople(_peopleViewModel);
        }
    }
}