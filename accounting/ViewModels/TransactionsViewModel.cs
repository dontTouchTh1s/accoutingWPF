using System.Collections.ObjectModel;
using accounting.Models;

namespace accounting.ViewModels
{
    public class TransactionsViewModel : BaseViewModel
    {
        private int _fundAccountName;
        private int _amount;
        private ObservableCollection<AccountsModel> _accountsList;

        public int FundAccountName
        {
            get => _fundAccountName;
            set
            {
                SetProperty(ref _fundAccountName, value);
            }
        }
        public int Amount
        {
            get => _amount;
            set
            {
                SetProperty(ref _amount, value);
            }
        }

        public ObservableCollection<AccountsModel> AccountsList
        {
            get => _accountsList;
            set
            {
                SetProperty(ref _accountsList, value);
            }
        }
    }
}