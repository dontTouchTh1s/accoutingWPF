using System.Globalization;
using SSWheatAdmin.Models;
using SSWheatAdmin.ViewModels.ManageAccounts;
using SSWheatAdmin.ViewModels.ManageLoans;

namespace SSWheatAdmin.Commands.CurrencyComboBoxCommands
{
    public class CreditLostFocusCommand : BaseCommand
    {
        private readonly CreateAccountViewModel _createAccountViewModel;
        private readonly InstalmentLoanViewModel? _instalmentLoanViewModel;

        public CreditLostFocusCommand(InstalmentLoanViewModel instalmentLoanViewModel)
        {
            _instalmentLoanViewModel = instalmentLoanViewModel;
        }

        public CreditLostFocusCommand(CreateAccountViewModel createAccountViewModel)
        {
            _createAccountViewModel = createAccountViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (_instalmentLoanViewModel != null)
            {
                if (_instalmentLoanViewModel.Amount <
                    ulong.Parse(_instalmentLoanViewModel.CurrentSelectedLoan.MinimumInstalmentAmount, NumberStyles.Number, CultureInfo.CurrentCulture))
                    _instalmentLoanViewModel.AmountView =
                        _instalmentLoanViewModel.CurrentSelectedLoan.MinimumInstalmentAmount;
                return;
            }

            if (_createAccountViewModel.Credit < InvestmentFundModel.MinimumCredit)
                _createAccountViewModel.CreditView = "500000";
        }
    }
}