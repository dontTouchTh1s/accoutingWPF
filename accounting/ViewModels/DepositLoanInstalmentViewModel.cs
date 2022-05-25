using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using accounting.Commands;
using accounting.Commands.CurrencyComboBoxCommands;
using accounting.Models;
using accounting.ViewModels.ComboBoxItems;

namespace accounting.ViewModels
{
    public class DepositLoanInstalmentViewModel : BaseViewModel
    {
        private ObservableCollection<LoanItemViewModel> _loanList;
        private string? _loanSearchText;
        private ushort? _loanId;
        private string _amountView = "0";
        private string? _personalAccountNumber;
        private ObservableCollection<AccountsItemsViewModel> _accountList;
        private string? _accountSearchText;
        private ushort? _fundAccountId;
        private readonly List<AccountsItemsViewModel> _accountsItemsViewModels = new ();
        private readonly InvestmentFundModel _investmentFundModel;
        private readonly List<LoanItemViewModel> _loanItemsViewModels = new();

        public DepositLoanInstalmentViewModel(InvestmentFundModel investmentFundModel)
        {
            CreditPreviewKeyDownCommand = new CreditPreviewKeyDownCommand();
            CreditPreviewKeyUpCommand = new CreditPreviewKeyUpCommand();
            PayLoanInstalment = new PayLoanInstalmentCommand();
            
            _investmentFundModel = investmentFundModel;
            AmountHelperText = string.Format("حداقل مبلغ قسط {0} تومان است.", 4);
            AmountView = "0";
            GetAccounts();
            GetAccountLoans();
        }

        private async void GetAccountLoans()
        {
            var loanModels = await _investmentFundModel.GetAccountLoans(FundAccountId);
            foreach (var loan in loanModels)
            {
                _loanItemsViewModels.Add(new LoanItemViewModel(loan.Id, loan.Amount, loan.InstallmentsCount));
            }

            LoansList = new ObservableCollection<LoanItemViewModel>(_loanItemsViewModels);

        }

        public string? AccountOwnerFullName { get; set; }

        public ObservableCollection<LoanItemViewModel> LoansList
        {
            get => _loanList;
            set => SetProperty(ref _loanList, value);
        }

        public ushort? LoanId
        {
            get => _loanId;
            set => SetProperty(ref _loanId, value);
        }
        public string? LoanSearchText
        {
            get => _loanSearchText;
            set => SetProperty(ref _loanSearchText, value);
        }

        public string AmountHelperText { get; }

        public ICommand CreditPreviewKeyUpCommand { get; set; }

        public ICommand CreditPreviewKeyDownCommand { get; set; }

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

        public ICommand PayLoanInstalment { get; set; }

        public ObservableCollection<AccountsItemsViewModel> AccountsList
        {
            get => _accountList;
            set
            {
                SetProperty(ref _accountList, value);
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

        public ushort? FundAccountId
        {
            get => _fundAccountId;
            set => SetProperty(ref (_fundAccountId), value);
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
        private async void GetAccounts()
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