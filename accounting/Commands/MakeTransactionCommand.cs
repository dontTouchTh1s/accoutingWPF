using System;
using System.Threading.Tasks;
using System.Windows.Media;
using accounting.Models;
using accounting.ViewModels;
using accounting.ViewModels.Dialogs;
using MaterialDesignThemes.Wpf;

namespace accounting.Commands
{
    public class MakeTransactionCommand : BaseAsyncCommand
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private readonly TransactionsViewModel _transactionViewModel;

        public MakeTransactionCommand(TransactionsViewModel transactionsViewModel,
            InvestmentFundModel transactionsModel)
        {
            _transactionViewModel = transactionsViewModel;
            _investmentFundModel = transactionsModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if ((string)_transactionViewModel.TransactionType! == "Withdraw")
                _transactionViewModel.Amount *= -1;
            try
            {
                await _investmentFundModel.MakeTransaction(_transactionViewModel);
            }
            catch (Exception e)
            {
                var dialogViewModel =
                    new MessageDialogViewModel("خطایی در ثبت اطالاعات رخ داده است.",
                        PackIconKind.WarningCircle, new SolidColorBrush(Colors.Red));
                await DialogHost.Show(dialogViewModel, "rootDialog");
            }
        }
    }
}