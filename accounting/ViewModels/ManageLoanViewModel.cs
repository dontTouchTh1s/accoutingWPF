using System.Windows.Input;
using accounting.Commands;
using accounting.Models;
using accounting.Store;

namespace accounting.ViewModels
{
    public class ManageLoanViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private BaseViewModel _currentViewModel;

        public ManageLoanViewModel(InvestmentFundModel investmentFundModel, NavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToDepositCommand = new NavigatoinCommand(navigationService, _navigationService.NavigateToLendLoan);
            NavigateToLendCommand = new NavigatoinCommand(navigationService, _navigationService.NavigateToDepositLoan);
            _navigationService.CurrentViewChanged += OnViewChange;
            _currentViewModel = _navigationService.ManageLoansCurrentViewModel;
            CurrentViewModel = _currentViewModel;
        }

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public ICommand NavigateToDepositCommand { get; }
        public ICommand NavigateToLendCommand { get; }

        private void OnViewChange()
        {
            CurrentViewModel = _navigationService.ManageLoansCurrentViewModel;
        }
    }
}