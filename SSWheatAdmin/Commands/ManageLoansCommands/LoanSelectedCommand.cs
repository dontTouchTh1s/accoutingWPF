using System.Windows.Controls;
using SSWheatAdmin.Models;
using SSWheatAdmin.ViewModels.ManageLoans;
using SSWheatAdmin.ViewModels.MoreInfoWindowViewModel;

namespace SSWheatAdmin.Commands.ManageLoansCommands
{
    public class LoanSelectedCommand : BaseCommand
    {
        private readonly InvestmentFundModel _investmentFundModel;

        public LoanSelectedCommand(InvestmentFundModel investmentFundModel)
        {
            _investmentFundModel = investmentFundModel;
        }
          
        public override void Execute(object? parameter)
        {
            if (parameter == null) return;
            var args = (SelectionChangedEventArgs)parameter;
            if (args.AddedItems.Count == 0) return;
            if (args.AddedItems[0] == null) return;
            var item = args.AddedItems[0] as ViewLoanItemViewModel;
            var loanMoreInfoViewModel = new LoanMoreInfoViewModel(_investmentFundModel, item, item.Id);
            App.WindowsNavigationServices.OpenMoreInfoWindow(loanMoreInfoViewModel);
        }
    }
}