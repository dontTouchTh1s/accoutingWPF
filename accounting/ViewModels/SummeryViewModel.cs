using System.Threading.Tasks;
using accounting.Models;

namespace accounting.ViewModels
{
    public class SummeryViewModel : BaseViewModel
    {
        private int _balance;
        private readonly InvestmentFundModel _investmentFundModel;

        public SummeryViewModel(InvestmentFundModel investmentFundModel)
        {
            _investmentFundModel = investmentFundModel;
#pragma warning disable CS4014
            GetBalance();
#pragma warning restore CS4014
        }

        public int Balance
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }

        public async Task GetBalance()
        {
            Balance = await _investmentFundModel.GetBalance();
        }
    }
}