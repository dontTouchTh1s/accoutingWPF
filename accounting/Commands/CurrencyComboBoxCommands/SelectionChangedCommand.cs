using System.Windows.Controls;
using accounting.ViewModels;
using accounting.ViewModels.ComboBoxItems;

namespace accounting.Commands.CurrencyComboBoxCommands
{
    public class SelectionChangedCommand : BaseCommand
    {
        private readonly BaseViewModel _viewModel;

        public SelectionChangedCommand(BaseViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            var args = (SelectionChangedEventArgs)parameter!;
            var items = args.AddedItems;
            if (items.Count != 0)
            {
                if (items[0] is AccountsItemsViewModel item) _viewModel.HelperTextChange(item.AccountOwnerFullName);
            }
            else
            {
                _viewModel.HelperTextChange("");
            }
        }
    }
}