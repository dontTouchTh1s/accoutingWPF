using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using accounting.Commands;
using accounting.Commands.CurrencyComboBoxCommands;
using accounting.Commands.ManageLoansCommands;
using accounting.Models;
using accounting.ViewModels.ComboBoxItems;

namespace accounting.ViewModels.ManageLoans
{
    public class InstalmentLoanViewModel : BaseViewModel
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private ObservableCollection<AccountsItemsViewModel> _accountList;
        private ObservableCollection<LoanItemViewModel> _loanList;
        private List<AccountsItemsViewModel> _accountsItemsViewModels = new();
        private List<LoanItemViewModel> _loanItemsViewModels = new();
        private string? _accountSearchText;
        private string? _loanSearchText;

        private ushort? _loanId;
        private ushort? _fundAccountId;
        private string? _accountOwnerFullName;
        private string _amountView = "0";
        private string? _personalAccountNumber;
        private string _amountHelperText;
        private ulong _currentLoanMinimumInstalment;
        

        public InstalmentLoanViewModel(InvestmentFundModel investmentFundModel)
        {
            PayLoanInstalment = new PayLoanInstalmentCommand(this, investmentFundModel);
            CreditPreviewKeyDownCommand = new CreditPreviewKeyDownCommand();
            SelectedLoanChangedCommand = new SelectedLoanChangedCommand(this);
            SelectionChangedCommand = new SelectionChangedCommand(this);
            InstalmentAmountLostFocuse = new CreditLostFocusCommand(this);
            _investmentFundModel = investmentFundModel;
            AmountView = "0";
            _accountList = AccountsList;
            _loanList = LoansList;
            AmountHelperText = "";
            UpdateContent();
        }

        public ICommand CreditPreviewKeyDownCommand { get; set; }
        public ICommand PayLoanInstalment { get; }
        public ICommand SelectedLoanChangedCommand { get; }
        public ICommand SelectionChangedCommand { get; }
        public ICommand InstalmentAmountLostFocuse { get; }

        public string AmountHelperText
        {
            get => _amountHelperText;
            set => SetProperty(ref _amountHelperText, value);
        }

        public string? AccountOwnerFullName
        {
            get => _accountOwnerFullName;
            set => SetProperty(ref _accountOwnerFullName, value);
        }

        public ulong CurrentLoanMinimumInstalment
        {
            get => _currentLoanMinimumInstalment;
            set
            {
                var currencyValue = value.ToString("N0", CultureInfo.CurrentCulture);
                _currentLoanMinimumInstalment = value;
                AmountHelperText = string.Format("حداقل میزان پرداختی هر قسط {0} تومان است.", currencyValue);
                AmountView = currencyValue;
            }
        }

        public ObservableCollection<LoanItemViewModel> LoansList
        {
            get => _loanList;
            private set => SetProperty(ref _loanList, value);
        }

        public ObservableCollection<AccountsItemsViewModel> AccountsList
        {
            get => _accountList;
            private set => SetProperty(ref _accountList, value);
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

        public ulong Amount { get; private set; }

        public string? PersonalAccountNumber
        {
            get => _personalAccountNumber;
            set => SetProperty(ref _personalAccountNumber, value);
        }

        public ushort? LoanId
        {
            get => _loanId;
            set => SetProperty(ref _loanId, value);
        }

        public string? LoanSearchText
        {
            get => _loanSearchText;
            set
            {
                SetProperty(ref _loanSearchText, value);
                try
                {
                    LoanId = ushort.Parse(value!);
                }
                catch
                {
                    LoanId = null;
                }
            }
        }

        public ushort? FundAccountId
        {
            get => _fundAccountId;
            set
            {
                SetProperty(ref _fundAccountId, value);
                GetAccountLoans();
            }
        }

        public string? AccountSearchText
        {
            get => _accountSearchText;
            set
            {
                SetProperty(ref _accountSearchText, value);
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

        private void FilterAccountsList()
        {
            AccountsList = new ObservableCollection<AccountsItemsViewModel>(_accountsItemsViewModels);
            if (AccountSearchText != null)
                foreach (var accountsItemsViewModel in _accountsItemsViewModels)
                    if (!accountsItemsViewModel.AccountOwnerFullName.Contains(AccountSearchText) &&
                        !accountsItemsViewModel.AccountId.ToString().Contains(AccountSearchText) &&
                        !accountsItemsViewModel.AccountOwnerNationalId.Contains(AccountSearchText))
                        AccountsList.Remove(accountsItemsViewModel);
        }

        private async void GetAccountLoans()
        {
            LoansList = new ObservableCollection<LoanItemViewModel>();
            _loanItemsViewModels = new List<LoanItemViewModel>();
            var loanModels = await _investmentFundModel.GetAccountLoans(FundAccountId);
            foreach (var loan in loanModels)
                _loanItemsViewModels.Add(new LoanItemViewModel(loan.Id, loan.Amount, loan.InstallmentsCount));

            LoansList = new ObservableCollection<LoanItemViewModel>(_loanItemsViewModels);
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