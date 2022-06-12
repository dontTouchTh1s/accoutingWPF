using SSWheatAdmin.Models;

namespace SSWheatAdmin.ViewModels.ManageLoans
{
    public class ManageLoanViewModel : BaseViewModel
    {
        public ManageLoanViewModel(InvestmentFundModel investmentFundModel)
        {
            LendLoanViewModel = new LendLoanViewModel(investmentFundModel);
            InstalmentLoanViewModel = new InstalmentLoanViewModel(investmentFundModel);
            ViewLoanViewModle = new ViewLoansViewModel(investmentFundModel);
        }

        public LendLoanViewModel LendLoanViewModel { get; }
        public InstalmentLoanViewModel InstalmentLoanViewModel { get; }

        public ViewLoansViewModel ViewLoanViewModle { get; }

        public sealed override void UpdateContent()
        {
            LendLoanViewModel.UpdateContent();
            InstalmentLoanViewModel.UpdateContent();
            ViewLoanViewModle.UpdateContent();
        }
    }
}