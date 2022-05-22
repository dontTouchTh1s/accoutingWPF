using System.Windows.Controls;
using accounting.Models;
using accounting.ViewModels;

namespace accounting.Commands
{
    public class AccountIdTextChangedCommand : BaseCommand
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private readonly TransactionsViewModel _transactionViewModel;

        public AccountIdTextChangedCommand(TransactionsViewModel transactionsViewModel,
            InvestmentFundModel investmentFundModel)
        {
            _transactionViewModel = transactionsViewModel;
            _investmentFundModel = investmentFundModel;
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