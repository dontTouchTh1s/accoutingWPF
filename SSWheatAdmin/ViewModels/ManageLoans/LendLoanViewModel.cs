﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using SSWheatAdmin.Commands;
using SSWheatAdmin.Commands.CurrencyComboBoxCommands;
using SSWheatAdmin.Models;
using SSWheatAdmin.ViewModels.ComboBoxItems;

namespace SSWheatAdmin.ViewModels.ManageLoans
{
    public class LendLoanViewModel : BaseViewModel
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private ObservableCollection<AccountsItemsViewModel> _accountList;
        private string? _accountOwnerFullName;
        private List<AccountsItemsViewModel> _accountsItemsViewModels = new();
        private string _amountView = "0";
        private byte? _fundAccountId;
        private byte? _instalmentCount;
        private string? _personalAccountNumber;
        private string? _searchText;

        public LendLoanViewModel(InvestmentFundModel investmentFundModel)
        {
            _investmentFundModel = investmentFundModel;
            _accountList = AccountsList;
            LendLoanCommand = new LendLoanCommand(this, investmentFundModel);
            CreditPreviewKeyDownCommand = new CreditPreviewKeyDownCommand();
            SelectionChangedCommand = new SelectionChangedCommand(this);
            Amount = 0;
            UpdateContent();
        }

        public ICommand LendLoanCommand { get; }
        public ICommand CreditPreviewKeyDownCommand { get; }
        public ICommand SelectionChangedCommand { get; }

        public string? PersonalAccountNumber
        {
            get => _personalAccountNumber;
            set => SetProperty(ref _personalAccountNumber, value);
        }

        public string AmountView
        {
            get => _amountView;
            set
            {
                ulong.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out var provider);
                Amount = provider;
                value = provider.ToString("N0", CultureInfo.CurrentCulture);
                SetProperty(ref _amountView, value);
            }
        }

        public ulong? Amount { get; private set; }

        public byte? FundAccountId
        {
            get => _fundAccountId;
            set => SetProperty(ref _fundAccountId, value);
        }

        public string? SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                try
                {
                    FundAccountId = byte.Parse(value!);
                }
                catch
                {
                    FundAccountId = null;
                }

                FilterAccountsList();
            }
        }

        public ObservableCollection<AccountsItemsViewModel> AccountsList
        {
            get => _accountList;
            set => SetProperty(ref _accountList, value);
        }

        public string? AccountOwnerFullName
        {
            get => _accountOwnerFullName;
            set => SetProperty(ref _accountOwnerFullName, value);
        }

        public byte? InstalmentCount
        {
            get => _instalmentCount;
            set => SetProperty(ref _instalmentCount, value);
        }

        private void FilterAccountsList()
        {
            AccountsList = new ObservableCollection<AccountsItemsViewModel>(_accountsItemsViewModels);
            if (SearchText != null)
                foreach (var accountsItemsViewModel in _accountsItemsViewModels)
                    if (!accountsItemsViewModel.AccountOwnerFullName.Contains(SearchText) &&
                        !accountsItemsViewModel.AccountId.ToString().Contains(SearchText) &&
                        !accountsItemsViewModel.AccountOwnerNationalId.Contains(SearchText))
                        AccountsList.Remove(accountsItemsViewModel);
        }

        private async void GetAccounts()
        {
            AccountsList = new ObservableCollection<AccountsItemsViewModel>();
            var peoplesAccounts = await _investmentFundModel.GetAllPeoplesAccounts();
            AccountsList = ConvertToItemViewModelList(peoplesAccounts);
        }

        private ObservableCollection<AccountsItemsViewModel> ConvertToItemViewModelList(
            Dictionary<PeoplesModel, IEnumerable<AccountsModel>>? peoplesAccounts)
        {
            _accountsItemsViewModels = new List<AccountsItemsViewModel>();
            foreach (var vPeoples in peoplesAccounts!)
            foreach (var account in vPeoples.Value)
            {
                var accountsItemsViewModels =
                    new AccountsItemsViewModel(account.Id, vPeoples.Key.Name + " " + vPeoples.Key.LastName,
                        vPeoples.Key.NationalId);
                _accountsItemsViewModels.Add(accountsItemsViewModels);
            }

            return new ObservableCollection<AccountsItemsViewModel>(_accountsItemsViewModels);
        }

        public override void HelperTextChange(string text)
        {
            AccountOwnerFullName = text;
        }

        public sealed override void UpdateContent()
        {
            GetAccounts();
        }
    }
}