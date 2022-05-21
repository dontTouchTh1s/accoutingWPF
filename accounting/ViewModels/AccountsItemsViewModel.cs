namespace accounting.ViewModels
{
    public class AccountsItemsViewModel : BaseViewModel
    {
        private int _accountId;
        private string _accountOwnerName;
        private string _accountOwnerNationalId;

        public AccountsItemsViewModel(int accountId, string accountOwnerName, string accountOwnerNationalId)
        {
            _accountId = accountId;
            _accountOwnerName = accountOwnerName;
            _accountOwnerNationalId = accountOwnerNationalId;
        }

        public int AccountId
        {
            get => _accountId;
            set => SetProperty(ref _accountId, value);
        }

        public string AccountOwnerFullName
        {
            get => _accountOwnerName;
            set => SetProperty(ref _accountOwnerName, value);
        }

        public string AccountOwnerNationalId
        {
            get => _accountOwnerNationalId;
            set => SetProperty(ref _accountOwnerNationalId, value);
        }
    }
}