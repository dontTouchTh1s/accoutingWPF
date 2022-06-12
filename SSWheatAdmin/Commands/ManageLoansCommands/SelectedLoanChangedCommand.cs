using System.Globalization;
using System.Windows.Controls;
using SSWheatAdmin.ViewModels.ComboBoxItems;
using SSWheatAdmin.ViewModels.ManageLoans;

namespace SSWheatAdmin.Commands.ManageLoansCommands
{
    public class SelectedLoanChangedCommand : BaseCommand
    {
        private readonly InstalmentLoanViewModel _instalmentLoanViewModel;

        public SelectedLoanChangedCommand(InstalmentLoanViewModel instalmentLoanViewModel)
        {
            _instalmentLoanViewModel = instalmentLoanViewModel;
        }

        public override void Execute(object? parameter)
        {
            var args = (SelectionChangedEventArgs)parameter!;
            if (args.AddedItems.Count == 0) return;
            var item = (LoanItemViewModel)args.AddedItems[0]!;
            _instalmentLoanViewModel.CurrentSelectedLoan =
                item;
        }
    }
}