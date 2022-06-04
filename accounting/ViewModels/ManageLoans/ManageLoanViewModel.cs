using accounting.Models;

namespace accounting.ViewModels.ManageLoans
{
    public class ManageLoanViewModel : BaseViewModel
    {
        public ManageLoanViewModel(InvestmentFundModel investmentFundModel)
        {
            LendLoanViewModel = new LendLoanViewModel(investmentFundModel);
            DepositLoanInstalmentViewModel = new DepositLoanInstalmentViewModel(investmentFundModel);
            ViewLoanViewModle = new ViewLoansViewModel(investmentFundModel);
        }

        public LendLoanViewModel LendLoanViewModel { get; }
        public DepositLoanInstalmentViewModel DepositLoanInstalmentViewModel { get; }

        public ViewLoansViewModel ViewLoanViewModle { get; }

        public sealed override void UpdateContent()
        {
            LendLoanViewModel.UpdateContent();
            DepositLoanInstalmentViewModel.UpdateContent();
            ViewLoanViewModle.UpdateContent();
        }
    }
}