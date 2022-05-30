using System;
using accounting.ViewModels;

namespace accounting.Store
{
    public class NavigationService
    {
        private BaseViewModel _manageLoansCurrentViewModel = null!;

        public NavigationService(params BaseViewModel[] ViewModels)
        {
            SummeryViewModel = (SummeryViewModel)ViewModels[0];
            TransactionsViewModel = (TransactionsViewModel)ViewModels[1];
            ManageLoanViewModel = (ManageLoanViewModel)ViewModels[2];
            CreateAccountViewModel = (CreateAccountViewModel)ViewModels[3];
        }
        public BaseViewModel CurrentViewModel
        {
            get => _manageLoansCurrentViewModel;
            set
            {
                _manageLoansCurrentViewModel = value;
                OnCurrentViewChanged();
            }
        }

        public ManageLoanViewModel ManageLoanViewModel { get; set; }
        public SummeryViewModel SummeryViewModel { get; set; }
        public TransactionsViewModel TransactionsViewModel { get; set; }
        public CreateAccountViewModel CreateAccountViewModel { get; }

        private void OnCurrentViewChanged()
        {
            CurrentViewChanged?.Invoke();
        }

        public void Navigate(BaseViewModel navigationDestination)
        {
            CurrentViewModel = navigationDestination;
        }

        public event Action CurrentViewChanged = null!;
    }
}