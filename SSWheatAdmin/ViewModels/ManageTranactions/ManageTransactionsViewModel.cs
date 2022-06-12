using SSWheatAdmin.Models;

namespace SSWheatAdmin.ViewModels.ManageTranactions
{
    public class ManageTransactionsViewModel : BaseViewModel
    {
        public ManageTransactionsViewModel(InvestmentFundModel investmentFundModel)
        {
            TransactionsViewModel = new TransactionsViewModel(investmentFundModel);
            ViewTransactionsViewModel = new ViewTransactionsViewModel(investmentFundModel);
        }

        public TransactionsViewModel TransactionsViewModel { get; }
        public ViewTransactionsViewModel ViewTransactionsViewModel { get; }

        public override void UpdateContent()
        {
            TransactionsViewModel.UpdateContent();
            ViewTransactionsViewModel.UpdateContent();
        }
    }
}