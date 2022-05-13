using accounting.Models;

namespace accounting.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(InvestmentFundModel investmentFundModel)
        {
            CreateAccountViewModel = new CreateAccountViewModel(investmentFundModel);
        }

        public CreateAccountViewModel CreateAccountViewModel { get; }
    }
}