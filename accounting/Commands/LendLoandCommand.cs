using System;
using System.Threading.Tasks;
using System.Windows.Media;
using accounting.Models;
using accounting.ViewModels;
using accounting.ViewModels.Dialogs;
using MaterialDesignThemes.Wpf;

namespace accounting.Commands
{
    public class LendLoanCommand : BaseAsyncCommand
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private readonly LendLoanViewModel _lendLoanViewModel;

        public LendLoanCommand(LendLoanViewModel lendLoanViewModel, InvestmentFundModel investmentFundModel)
        {
            _lendLoanViewModel = lendLoanViewModel;
            _investmentFundModel = investmentFundModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _investmentFundModel.LendLoan(_lendLoanViewModel);
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