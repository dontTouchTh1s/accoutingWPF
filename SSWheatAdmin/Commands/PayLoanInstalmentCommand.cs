using System;
using System.Threading.Tasks;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using SSWheatAdmin.Exceptions;
using SSWheatAdmin.Models;
using SSWheatAdmin.ViewModels.Dialogs;
using SSWheatAdmin.ViewModels.ManageLoans;

namespace SSWheatAdmin.Commands
{
    public class PayLoanInstalmentCommand : BaseAsyncCommand
    {
        private readonly InstalmentLoanViewModel _instalmentLoanViewModel;
        private readonly InvestmentFundModel _investmentFundModel;

        public PayLoanInstalmentCommand(InstalmentLoanViewModel instalmentLoanViewModel,
            InvestmentFundModel investmentFundModel)
        {
            _investmentFundModel = investmentFundModel;
            _instalmentLoanViewModel = instalmentLoanViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                if (!await _investmentFundModel.IsInstalmentAmountValid(_instalmentLoanViewModel.Amount,
                        _instalmentLoanViewModel.LoanId ?? 0))
                    _instalmentLoanViewModel.Amount = await
                        _investmentFundModel.GetLoanRemainedAmount(_instalmentLoanViewModel.LoanId ?? 0);
                await _investmentFundModel.PayLoanInstalment(_instalmentLoanViewModel);
                var dialogViewModel = new MessageDialogViewModel("با موفقیت پرداخت شد.",
                    PackIconKind.Check, new SolidColorBrush(Colors.Green));
                await DialogHost.Show(dialogViewModel, "rootDialog");
            }
            catch (NotEnoughAvailableCreditException)
            {
                var dialogViewModel =
                    new MessageDialogViewModel("اعتبار کافی برای پرداخت از حساب صندوق وجود ندارد.",
                        PackIconKind.WarningCircle, new SolidColorBrush(Colors.Red));
                await DialogHost.Show(dialogViewModel, "rootDialog");
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