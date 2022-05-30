using System.Windows.Controls;
using accounting.Store;
using accounting.ViewModels;

namespace accounting.Commands
{
    public class CheckTabItemUpdateCommand : BaseCommand
    {
        private readonly MainViewModel _mainViewModel;
        private readonly NavigationService _navigatoinSerivce;

        public CheckTabItemUpdateCommand(MainViewModel mainViewModel, NavigationService navigationService)
        {
            _navigatoinSerivce = navigationService;
            _mainViewModel = mainViewModel;
        }

        public override void Execute(object? parameter)
        {
            var args = (SelectionChangedEventArgs)parameter!;
            try
            {
                var selectedItem = (TabItem)args.AddedItems[0]!;

                switch (selectedItem.Name)
                {
                    case "Home":
                    {
                        _mainViewModel.SummeryViewModel.UpdateContent();
                        break;
                    }
                    case "CreateAccount":
                    {
                        _mainViewModel.CreateAccountViewModel.UpdateContent();
                        break;
                    }
                    case "Transactions":
                    {
                        _mainViewModel.TransactionsViewModel.UpdateContent();
                        break;
                    }
                    case "ManageLoans":
                    {
                        _navigatoinSerivce.DepositLandViewModel.UpdateContent();
                        _navigatoinSerivce.LendLoanViewModel.UpdateContent();
                        break;
                    }
                    case "PeoplesAndAcconuts":
                    {
                        //
                        break;
                    }
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}