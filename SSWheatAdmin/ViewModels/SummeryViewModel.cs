using SSWheatAdmin.Models;

namespace SSWheatAdmin.ViewModels
{
    public class SummeryViewModel : BaseViewModel
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private string _availableBalance;
        private ulong _balance;

        public SummeryViewModel(InvestmentFundModel investmentFundModel)
        {
            _investmentFundModel = investmentFundModel;
            UpdateContent();
        }

        public ulong Balance
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }

        public string AvailableBalance
        {
            get => _availableBalance;
            set => SetProperty(ref _availableBalance, value);
        }

        private async void GetBalance()
        {
            Balance = await _investmentFundModel.GetBalance();
        }

        private async void GetAvailableBalance()
        {
            AvailableBalance = (await _investmentFundModel.GetAvailableBalance()).ToString();
        }

        public sealed override void UpdateContent()
        {
            GetBalance();
            GetAvailableBalance();
        }
    }
}