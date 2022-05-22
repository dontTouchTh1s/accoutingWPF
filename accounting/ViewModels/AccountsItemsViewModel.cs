﻿namespace accounting.ViewModels
{
    public class AccountsItemsViewModel : BaseViewModel
    {
        private int _accountId;
        private string _accountOwnerFullName;
        private string _accountOwnerNationalId;

        public AccountsItemsViewModel(int accountId, string accountOwnerFullName, string accountOwnerNationalId)
        {
            _accountId = accountId;
            _accountOwnerFullName = accountOwnerFullName;
            _accountOwnerNationalId = accountOwnerNationalId;
        }

        public int AccountId
        {
            get => _accountId;
            set => SetProperty(ref _accountId, value);
        }

        public string AccountOwnerFullName
        {
            get => _accountOwnerFullName;
            set => SetProperty(ref _accountOwnerFullName, value);
        }

        public string AccountOwnerNationalId
        {
            get => _accountOwnerNationalId;
            set => SetProperty(ref _accountOwnerNationalId, value);
        }

        public override string ToString()
        {
            return AccountId.ToString();
        }
    }
}