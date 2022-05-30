using System.Windows.Input;
using accounting.Commands;
using accounting.Models;
using accounting.Store;

namespace accounting.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;

        private BaseViewModel _manageLoanViewModel = null!;

        public MainViewModel(InvestmentFundModel investmentFundModel, NavigationService navigationService)
        {
            _navigationService = navigationService;
            ManageLoanViewModel = new ManageLoanViewModel(investmentFundModel, navigationService);
            CheckTabItemUpdateCommand = new CheckTabItemUpdateCommand(this, _navigationService);
            TransactionsViewModel = new TransactionsViewModel(investmentFundModel);
            CreateAccountViewModel = new CreateAccountViewModel(investmentFundModel);
            SummeryViewModel = new SummeryViewModel(investmentFundModel);
        }

        public CreateAccountViewModel CreateAccountViewModel { get; }
        public SummeryViewModel SummeryViewModel { get; }
        public TransactionsViewModel TransactionsViewModel { get; }

        public LendLoanViewModel LendLoanViewModel { get; }

        public ICommand CheckTabItemUpdateCommand { get; }

        public BaseViewModel ManageLoanViewModel { get; }
    }
}