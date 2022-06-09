using System;
using System.Threading.Tasks;
using System.Windows.Media;
using accounting.Models;
using accounting.ViewModels.Dialogs;
using accounting.ViewModels.ManageLoans;
using MaterialDesignThemes.Wpf;

namespace accounting.Commands
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
                await _investmentFundModel.PayLoanInstalment(_instalmentLoanViewModel);
                var dialogViewModel = new MessageDialogViewModel("با موفقیت پرداخت شد.",
                    PackIconKind.Check, new SolidColorBrush(Colors.Green));
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