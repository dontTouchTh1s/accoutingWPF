using System.Windows.Controls;
using accounting.ViewModels;
using accounting.ViewModels.ComboBoxItems;

namespace accounting.Commands
{
    public class SelectionChangedCommand : BaseCommand
    {
        private readonly TransactionsViewModel _transactionViewModel;

        public SelectionChangedCommand(TransactionsViewModel transactionsViewModel)
        {
            _transactionViewModel = transactionsViewModel;
        }

        public override void Execute(object? parameter)
        {
            var args = (SelectionChangedEventArgs)parameter!;
            var items = args.AddedItems;
            if (items.Count != 0)
            {
                if (items[0] is AccountsItemsViewModel item)
                    _transactionViewModel.AccountOwnerFullName = item.AccountOwnerFullName;
            }
            else
            {
                _transactionViewModel.AccountOwnerFullName = "";
            }
        }
    }
}