using System;
using accounting.Models;
using accounting.ViewModels;

namespace accounting.Store
{
    public class NavigationService
    {
        private BaseViewModel _manageLoansCurrentViewModel = null!;

        public NavigationService(InvestmentFundModel investmentFundModel)
        {
            DepositLandViewModel = new DepositLoanInstalmentViewModel(investmentFundModel);
            LendLoanViewModel = new LendLoanViewModel(investmentFundModel, this);
            NavigateToLendLoan();
        }

        public BaseViewModel ManageLoansCurrentViewModel
        {
            get => _manageLoansCurrentViewModel;
            set
            {
                _manageLoansCurrentViewModel = value;
                OnCurrentViewChanged();
            }
        }

        public LendLoanViewModel LendLoanViewModel { get; set; }
        public DepositLoanInstalmentViewModel DepositLandViewModel { get; set; }

        private void OnCurrentViewChanged()
        {
            CurrentViewChanged?.Invoke();
        }

        public void NavigateToDepositLoan()
        {
            ManageLoansCurrentViewModel = DepositLandViewModel;
        }

        public void NavigateToLendLoan()
        {
            ManageLoansCurrentViewModel = LendLoanViewModel;
        }

        public event Action CurrentViewChanged = null!;
    }
}