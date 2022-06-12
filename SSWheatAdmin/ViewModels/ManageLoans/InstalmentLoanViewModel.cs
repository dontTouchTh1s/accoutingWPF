using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using SSWheatAdmin.Commands;
using SSWheatAdmin.Commands.CurrencyComboBoxCommands;
using SSWheatAdmin.Commands.ManageLoansCommands;
using SSWheatAdmin.Models;
using SSWheatAdmin.ViewModels.ComboBoxItems;

namespace SSWheatAdmin.ViewModels.ManageLoans
{
    public class InstalmentLoanViewModel : BaseViewModel
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private ObservableCollection<AccountsItemsViewModel> _accountList;
        private string? _accountOwnerFullName;
        private string? _accountSearchText;
        private List<AccountsItemsViewModel> _accountsItemsViewModels = new();
        private string _amountHelperText;
        private string _amountView = "0";
        private LoanItemViewModel _currentSelectedLoan;
        private ushort? _fundAccountId;

        private ushort? _loanId;
        private List<LoanItemViewModel> _loanItemsViewModels = new();
        private ObservableCollection<LoanItemViewModel> _loanList;
        private string? _loanSearchText;

        private bool _payFromFund;
        private string? _personalAccountNumber;

        private bool _personalAccountNumberIsEnable = true;


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

        public LoanItemViewModel CurrentSelectedLoan
        {
            get => _currentSelectedLoan;
            set
            {
                SetProperty(ref _currentSelectedLoan, value);
                var currencyValue = value.MinimumInstalmentAmount;
                AmountHelperText = string.Format("حداقل میزان پرداختی هر قسط {0} تومان است.", currencyValue);
                AmountView = currencyValue;
            }
        }

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

        public ulong Amount { get; set; }

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

        public bool PayFromFund
        {
            get => _payFromFund;
            set
            {
                SetProperty(ref _payFromFund, value);
                PersonalAccountNumberIsEnable = !value;
            }
        }

        public bool PersonalAccountNumberIsEnable
        {
            get => _personalAccountNumberIsEnable;
            private set => SetProperty(ref _personalAccountNumberIsEnable, value);
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
            var loanModels = await _investmentFundModel.GetAccountUnpaidLoans(FundAccountId);
            foreach (var loan in loanModels)
                _loanItemsViewModels.Add(new LoanItemViewModel(loan.Key.Id, loan.Key.Amount, loan.Key.InstallmentsCount,
                    loan.Value));

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