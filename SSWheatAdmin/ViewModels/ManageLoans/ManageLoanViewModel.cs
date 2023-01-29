using SSWheatAdmin.Models;

namespace SSWheatAdmin.ViewModels.ManageLoans
{
    public class ManageLoanViewModel : BaseViewModel
    {
        public ManageLoanViewModel(InvestmentFundModel investmentFundModel)
        {
            LendLoanViewModel = new LendLoanViewModel(investmentFundModel);
            InstalmentLoanViewModel = new InstalmentLoanViewModel(investmentFundModel);
            ViewLoanViewModel = new ViewLoansViewModel(investmentFundModel);
        }

        public LendLoanViewModel LendLoanViewModel { get; }
        public InstalmentLoanViewModel InstalmentLoanViewModel { get; }

        public ViewLoansViewModel ViewLoanViewModel { get; }

        public sealed override void UpdateContent()
        {
            LendLoanViewModel.UpdateContent();
            InstalmentLoanViewModel.UpdateContent();
            ViewLoanViewModel.UpdateContent();
        }
    }
}