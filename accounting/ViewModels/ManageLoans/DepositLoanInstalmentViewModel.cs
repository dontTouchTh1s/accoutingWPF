using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using accounting.Commands;
using accounting.Commands.CurrencyComboBoxCommands;
using accounting.Commands.ManageLoansCommands;
using accounting.Models;
using accounting.ViewModels.ComboBoxItems;

namespace accounting.ViewModels.ManageLoans
{
    public class DepositLoanInstalmentViewModel : BaseViewModel
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private ObservableCollection<AccountsItemsViewModel> _accountList;
        private string? _accountSearchText;
        private List<AccountsItemsViewModel> _accountsItemsViewModels = new();
        private string _amountView = "0";
        private ushort? _fundAccountId;
        private ushort? _loanId;
        private List<LoanItemViewModel> _loanItemsViewModels = new();
        private ObservableCollection<LoanItemViewModel> _loanList;
        private string? _loanSearchText;
        private string? _personalAccountNumber;
        private string _amountHelperText;
        private string? _accountOwnerFullName;

        public DepositLoanInstalmentViewModel(InvestmentFundModel investmentFundModel)
        {
            CreditPreviewKeyDownCommand = new CreditPreviewKeyDownCommand();
            PayLoanInstalment = new PayLoanInstalmentCommand();
            SelectedLoanChangedCommand = new SelectedLoanChangedCommand(this);
            SelectionChangedCommand = new SelectionChangedCommand(this);
            _investmentFundModel = investmentFundModel;
            AmountView = "0";
            _accountList = AccountsList;
            _loanList = LoansList;
            UpdateContent();
            AmountHelperText = "";
        }

        public ICommand CreditPreviewKeyDownCommand { get; set; }
        public ICommand PayLoanInstalment { get; }
        public ICommand SelectedLoanChangedCommand { get; }
        public ICommand SelectionChangedCommand { get; }

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
            set => SetProperty(ref _loanList, value);
        }

        public ObservableCollection<AccountsItemsViewModel> AccountsList
        {
            get => _accountList;
            set => SetProperty(ref _accountList, value);
        }

        public string AmountView
        {
            get => _amountView;
            set => SetProperty(ref _amountView, value);
        }

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