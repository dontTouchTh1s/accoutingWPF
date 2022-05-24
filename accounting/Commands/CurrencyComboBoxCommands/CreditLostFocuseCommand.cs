using accounting.ViewModels;

namespace accounting.Commands.CurrencyComboBoxCommands
{
    public class CreditLostFocusCommand : BaseCommand
    {
        private readonly CreateAccountViewModel _createAccountViewModel;

        public CreditLostFocusCommand(CreateAccountViewModel createAccountViewModel)
        {
            _createAccountViewModel = createAccountViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (_createAccountViewModel.Credit < 500000)
                _createAccountViewModel.CreditView = "500000";
        }
    }
}