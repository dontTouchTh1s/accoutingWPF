using SSWheatAdmin.Models;

namespace SSWheatAdmin.ViewModels.ManageAccounts
{
    public class ManageAccountsViewModel : BaseViewModel
    {
        public ManageAccountsViewModel(InvestmentFundModel investmentFundModel)
        {
            ViewAccountsViewModel = new ViewAccountsViewModel(investmentFundModel);
            CreateAccountViewModel = new CreateAccountViewModel(investmentFundModel);
        }

        public ViewAccountsViewModel ViewAccountsViewModel { get; }
        public CreateAccountViewModel CreateAccountViewModel { get; }

        public override void UpdateContent()
        {
            ViewAccountsViewModel.UpdateContent();
            CreateAccountViewModel.UpdateContent();
        }
    }
}