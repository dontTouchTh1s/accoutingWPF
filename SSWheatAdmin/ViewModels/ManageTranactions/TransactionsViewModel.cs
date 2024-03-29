﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using SSWheatAdmin.Commands;
using SSWheatAdmin.Commands.CurrencyComboBoxCommands;
using SSWheatAdmin.Models;
using SSWheatAdmin.ViewModels.ComboBoxItems;

namespace SSWheatAdmin.ViewModels.ManageTranactions
{
    public class TransactionsViewModel : BaseViewModel
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private ObservableCollection<AccountsItemsViewModel> _accountList;

        private string? _accountOwnerFullName;
        private List<AccountsItemsViewModel> _accountsItemsViewModels = new();
        private string _amountView = "0";
        private ushort? _fundAccountId;
        private string? _personalAccountNumber;
        private string? _searchText;
        private object? _transactionType;

        public TransactionsViewModel(InvestmentFundModel investmentFundModel)
        {
            _investmentFundModel = investmentFundModel;
            MakeTransactionsCommand = new MakeTransactionCommand(this, investmentFundModel);
            SelectionChanged = new SelectionChangedCommand(this);
            CreditPreviewKeyDownCommand = new CreditPreviewKeyDownCommand();
            _accountList = AccountsList;
            AmountView = "0";
            UpdateContent();
        }

        public ICommand? MakeTransactionsCommand { get; }
        public ICommand? SelectionChanged { get; }
        public ICommand CreditPreviewKeyDownCommand { get; }

        public ushort? FundAccountId
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
                    FundAccountId = ushort.Parse(value!);
                }
                catch
                {
                    FundAccountId = null;
                }

                FilterAccountsList();
            }
        }

        public string? AmountView
        {
            get => _amountView;
            set
            {
                long.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out var provider);
                Amount = provider;
                value = provider.ToString("N0", CultureInfo.CurrentCulture);
                SetProperty(ref _amountView, value);
                SetProperty(ref _amountView, value);
            }
        }

        public long? Amount { get; set; }

        public string? PersonalAccountNumber
        {
            get => _personalAccountNumber;
            set => SetProperty(ref _personalAccountNumber, value);
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

        public object? TransactionType
        {
            get => _transactionType;
            set => SetProperty(ref _transactionType, value);
        }

        private void FilterAccountsList()
        {
            AccountsList = new ObservableCollection<AccountsItemsViewModel>(_accountsItemsViewModels);
            if (SearchText == null) return;
            foreach (var accountsItemsViewModel in _accountsItemsViewModels.Where(accountsItemsViewModel =>
                         !accountsItemsViewModel.AccountOwnerFullName.Contains(SearchText) &&
                         !accountsItemsViewModel.AccountId.ToString().Contains(SearchText) &&
                         !accountsItemsViewModel.AccountOwnerNationalId.Contains(SearchText)))
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