using SSWheatAdmin.Store;
using SSWheatAdmin.ViewModels;

namespace SSWheatAdmin.Commands
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
            try
            {
                var selectedItem = (string)parameter!;

                switch (selectedItem)
                {
                    case "Home":
                    {
                        _navigatoinSerivce.Navigate(_navigatoinSerivce.SummeryViewModel);
                        _navigatoinSerivce.SummeryViewModel.UpdateContent();
                        break;
                    }
                    case "CreateAccount":
                    {
                        _navigatoinSerivce.Navigate(_navigatoinSerivce.ManageAccountsViewModel);
                        _navigatoinSerivce.ManageAccountsViewModel.UpdateContent();
                        break;
                    }
                    case "Transactions":
                    {
                        _navigatoinSerivce.Navigate(_navigatoinSerivce.ManageTransactionsViewModel);
                        _navigatoinSerivce.ManageTransactionsViewModel.UpdateContent();
                        break;
                    }
                    case "ManageLoans":
                    {
                        _navigatoinSerivce.Navigate(_navigatoinSerivce.ManageLoanViewModel);
                        _navigatoinSerivce.ManageLoanViewModel.UpdateContent();
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