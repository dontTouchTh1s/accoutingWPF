using accounting.Models;

namespace accounting.ViewModels
{
    public class SummeryViewModel : BaseViewModel
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private int _balance;

        public SummeryViewModel(InvestmentFundModel investmentFundModel)
        {
            _investmentFundModel = investmentFundModel;
            GetBalance();
        }

        public int Balance
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }

        public async void GetBalance()
        {
            Balance = await _investmentFundModel.GetBalance();
        }
    }
}