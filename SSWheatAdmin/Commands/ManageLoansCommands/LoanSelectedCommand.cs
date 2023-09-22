using System.Windows.Controls;
using System.Windows.Input;
using SSWheatAdmin.Models;
using SSWheatAdmin.ViewModels.ManageLoans;
using SSWheatAdmin.ViewModels.MoreInfoWindowViewModel;

namespace SSWheatAdmin.Commands.ManageLoansCommands
{
    public class LoanSelectedCommand : BaseCommand
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private readonly ViewLoanItemViewModel _viewLoadnItemViewModel;

        public LoanSelectedCommand(ViewLoanItemViewModel viewLoanItemViewModel, InvestmentFundModel investmentFundModel)
        {
            _investmentFundModel = investmentFundModel;
            _viewLoadnItemViewModel = viewLoanItemViewModel;
        }
          
        public override void Execute(object? parameter)
        {
            if (parameter == null) return;
            var args = (MouseButtonEventArgs)parameter;
            args.Handled = true;
            var loanMoreInfoViewModel = new LoanMoreInfoViewModel(_investmentFundModel, _viewLoadnItemViewModel, _viewLoadnItemViewModel.Id);
            App.WindowsNavigationServices.OpenMoreInfoWindow(loanMoreInfoViewModel);
        }
    }
}