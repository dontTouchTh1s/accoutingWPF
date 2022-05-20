using System.Collections.ObjectModel;
using System.Windows.Input;
using accounting.Commands;
using accounting.Models;

namespace accounting.ViewModels
{
    public class TransactionsViewModel : BaseViewModel
    {
        private AccountsModel _account;
        private int _amount;
        private int _fundAccountId;
        private string? _personalAccountNumber;

        public TransactionsViewModel(InvestmentFundModel investmentFundModel)
        {
            MakeTransactionsCommand = new MakeTransactionCommand(this, investmentFundModel);
        }

        public int FundAccountId
        {
            get => _fundAccountId;
            set => SetProperty(ref _fundAccountId, value);
        }

        public int Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        public AccountsModel Account
        {
            get => _account;
            set
            {
                SetProperty(ref _account, value);
            }
        }

        public string? PersonalAccountNumber
        {
            get => _personalAccountNumber;
            set => SetProperty(ref _personalAccountNumber, value);
        }

        public ICommand? MakeTransactionsCommand { get; }

        public ObservableCollection<AccountsModel> AccountsList { get; set; }
    }
}