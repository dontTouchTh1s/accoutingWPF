using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using accounting.Commands;
using accounting.Models;

namespace accounting.ViewModels
{
    public class LendLoanViewModel : BaseViewModel
    {
        private readonly List<AccountsItemsViewModel> _accountsItemsViewModels = new();
        private readonly InvestmentFundModel _investmentFundModel;
        private ObservableCollection<AccountsItemsViewModel> _accountList;
        private string? _accountOwnerFullName;
        private ulong? _amount;
        private byte? _fundAccountId;
        private byte? _instalmentCount;
        private string? _personalAccountNumber;
        private string? _searchText;

        public LendLoanViewModel(InvestmentFundModel investmentFundModel)
        {
            _investmentFundModel = investmentFundModel;
            LendLoanCommand = new LendLoanCommand(this, investmentFundModel);
            _accountList = AccountsList;
#pragma warning disable CS4014
            GetAccounts();
#pragma warning restore CS4014
        }

        public ICommand LendLoanCommand { get; }

        public string? PersonalAccountNumber
        {
            get => _personalAccountNumber;
            set => SetProperty(ref _personalAccountNumber, value);
        }

        public ulong? Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

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

        private async Task GetAccounts()
        {
            var peoplesAccounts = await _investmentFundModel.GetAllPeoplesAccounts();
            AccountsList = ConvertToItemViewModelList(peoplesAccounts);
        }

        private ObservableCollection<AccountsItemsViewModel> ConvertToItemViewModelList(
            Dictionary<PeoplesModel, IEnumerable<AccountsModel>>? peoplesAccounts)
        {
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
    }
}