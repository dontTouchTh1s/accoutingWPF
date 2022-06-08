using System.Windows.Controls;
using accounting.ViewModels.ComboBoxItems;
using accounting.ViewModels.ManageLoans;
using accounting.Views.ComboBoxItems;

namespace accounting.Commands.ManageLoansCommands
{
    public class SelectedLoanChangedCommand : BaseCommand
    {
        private readonly DepositLoanInstalmentViewModel _depositLoanInstalmentViewModel;

        public SelectedLoanChangedCommand(DepositLoanInstalmentViewModel depositLoanInstalmentViewModel)
        {
            _depositLoanInstalmentViewModel = depositLoanInstalmentViewModel;
        }

        public override void Execute(object? parameter)
        {
            var args = (SelectionChangedEventArgs)parameter!;
            if (args.AddedItems.Count == 0) return;
            var item = (LoanItemViewModel)args.AddedItems[0]!;
            _depositLoanInstalmentViewModel.AmountHelperText =
                string.Format("حداقل میزان قسط {0} تومان است.", item.MinimumInstalmentAmount.ToString());
        }
    }
}