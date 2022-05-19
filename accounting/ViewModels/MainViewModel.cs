using accounting.Models;

namespace accounting.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(InvestmentFundModel investmentFundModel)
        {
            CreateAccountViewModel = new CreateAccountViewModel(investmentFundModel);
            SummeryViewModel = new SummeryViewModel(investmentFundModel);

        }

        public CreateAccountViewModel CreateAccountViewModel { get; }
        public SummeryViewModel SummeryViewModel { get; }
    }
}