using System;
using accounting.Models;
using accounting.ViewModels;

namespace accounting.Store
{
    public class NavigationService
    {
        private readonly InvestmentFundModel _investmentFundModel;

        private BaseViewModel _manageLoansCurrentViewModel;

        public NavigationService(InvestmentFundModel investmentFundModel)
        {
            _investmentFundModel = investmentFundModel;
            DepositLandViewModel = new DepositLoanInstalmentViewModel(_investmentFundModel);
            LendLoanViewModel = new LendLoanViewModel(_investmentFundModel, this);
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

        public event Action CurrentViewChanged;
    }
}