using System.Windows.Input;
using accounting.Commands;
using accounting.Models;
using accounting.Store;

namespace accounting.ViewModels
{
    public class ManageLoanViewModel : BaseViewModel
    {

        public ManageLoanViewModel(InvestmentFundModel investmentFundModel)
        {
            LendLoanViewModel = new LendLoanViewModel(investmentFundModel);
            DepositLoanInstalmentViewModel = new DepositLoanInstalmentViewModel(investmentFundModel);

        }

        public LendLoanViewModel LendLoanViewModel { get; }
        public DepositLoanInstalmentViewModel DepositLoanInstalmentViewModel { get; }
    }
}