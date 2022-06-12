using System;
using SSWheatAdmin.ViewModels;
using SSWheatAdmin.ViewModels.ManageAccounts;
using SSWheatAdmin.ViewModels.ManageLoans;
using SSWheatAdmin.ViewModels.ManageTranactions;

namespace SSWheatAdmin.Store
{
    public class NavigationService
    {
        private BaseViewModel _manageLoansCurrentViewModel = null!;

        public NavigationService(params BaseViewModel[] viewModels)
        {
            SummeryViewModel = (SummeryViewModel)viewModels[0];
            ManageTransactionsViewModel = (ManageTransactionsViewModel)viewModels[1];
            ManageLoanViewModel = (ManageLoanViewModel)viewModels[2];
            ManageAccountsViewModel = (ManageAccountsViewModel)viewModels[3];
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

        public ManageLoanViewModel ManageLoanViewModel { get; }
        public SummeryViewModel SummeryViewModel { get; }
        public ManageTransactionsViewModel ManageTransactionsViewModel { get; }
        public ManageAccountsViewModel ManageAccountsViewModel { get; }

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