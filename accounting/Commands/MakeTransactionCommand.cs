using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Media;
using accounting.Exceptions;
using accounting.Models;
using accounting.ViewModels.Dialogs;
using accounting.ViewModels.ManageTranactions;
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
            _transactionViewModel.Amount = long.Parse(_transactionViewModel.AmountView ?? "0", NumberStyles.Number,
                CultureInfo.CurrentCulture);
            if ((string)_transactionViewModel.TransactionType! == "Withdraw")
                _transactionViewModel.Amount = -long.Parse(_transactionViewModel.AmountView ?? "0", NumberStyles.Number,
                    CultureInfo.CurrentCulture);
            try
            {
                await _investmentFundModel.MakeTransaction(_transactionViewModel);
                var dialogViewModel = new MessageDialogViewModel("تراکنش با موفقیت انجام شد.",
                    PackIconKind.Check, new SolidColorBrush(Colors.Green));
                await DialogHost.Show(dialogViewModel, "rootDialog");
            }
            catch (NotEnoughAvailableCreditException e)
            {
                var dialogViewModel =
                    new MessageDialogViewModel(e.Message,
                        PackIconKind.WarningCircle, new SolidColorBrush(Colors.Red));
                await DialogHost.Show(dialogViewModel, "rootDialog");
            }
            catch (Exception)
            {
                var dialogViewModel =
                    new MessageDialogViewModel("خطایی در ثبت اطالاعات رخ داده است.",
                        PackIconKind.WarningCircle, new SolidColorBrush(Colors.Red));
                await DialogHost.Show(dialogViewModel, "rootDialog");
            }
        }
    }
}