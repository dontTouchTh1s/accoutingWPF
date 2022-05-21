using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using accounting.Commands;
using accounting.Models;

namespace accounting.ViewModels
{
    public class TransactionsViewModel : BaseViewModel
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private AccountsModel _account;

        private ObservableCollection<AccountsItemsViewModel> _accountList;
        private int _amount;
        private int? _fundAccountId;
        private string? _personalAccountNumber;

        private string? _searchText;

        public TransactionsViewModel(InvestmentFundModel investmentFundModel)
        {
            _investmentFundModel = investmentFundModel;
            MakeTransactionsCommand = new MakeTransactionCommand(this, investmentFundModel);
            AccountIdTextChangedCommand = new AccountIdTextChangedCommand(this, investmentFundModel);
            GetAccounts();
        }

        public int? FundAccountId
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
            set => SetProperty(ref _account, value);
        }

        public string? PersonalAccountNumber
        {
            get => _personalAccountNumber;
            set => SetProperty(ref _personalAccountNumber, value);
        }

        public ICommand? MakeTransactionsCommand { get; }
        public ICommand? AccountIdTextChangedCommand { get; }

        public ObservableCollection<AccountsItemsViewModel> AccountsList
        {
            get => _accountList;
            set => SetProperty(ref _accountList, value);
        }

        public string? SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                try
                {
                    SetProperty(ref _fundAccountId, int.Parse(value));
                }
                catch (Exception e)
                {
                    SetProperty(ref _fundAccountId, null);
                }

                GetAccounts();
                FilterAccountsList();
            }
        }

        private async void FilterAccountsList()
        {
            if (SearchText != null)
            {
                var accountBuyOwnerList = await _investmentFundModel.FindPeoplesAccounts(SearchText);
                if (accountBuyOwnerList != null)
                {
                    AccountsList = new ObservableCollection<AccountsItemsViewModel>();
                    foreach (var vPeoples in accountBuyOwnerList)
                    {
                        foreach (var accounts in vPeoples.Value)
                            AddItem(vPeoples.Key.Name, vPeoples.Key.LastName, accounts.Id, vPeoples.Key.NationalId);
                    }
                    return;
                }
            }

            var similarItems = AccountsList.Where(r => r.AccountId.ToString()==SearchText).ToList();
            foreach (var item in similarItems)
            {
                AccountsList.Add(item);
            }
        }


        private async void GetAccounts()
        {
            var peoplesAccounts = await _investmentFundModel.GetAllPeoplesAccounts();
            AccountsList = new ObservableCollection<AccountsItemsViewModel>();
            foreach (var vPeoples in peoplesAccounts!)
            {
                foreach (var accounts in vPeoples.Value)
                    AddItem(vPeoples.Key.Name, vPeoples.Key.LastName, accounts.Id, vPeoples.Key.NationalId);
            }
        }

        private void AddItem(string name, string lastName, int accountId, string nationalId)
        {
            AccountsList.Add(new AccountsItemsViewModel(accountId, name + " " + lastName, nationalId));
        }
    }
}