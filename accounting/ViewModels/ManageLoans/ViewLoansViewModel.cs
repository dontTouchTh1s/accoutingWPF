using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using accounting.Models;

namespace accounting.ViewModels.ManageLoans
{
    public class ViewLoansViewModel : BaseViewModel
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private ObservableCollection<ViewLoanItemViewModel> _loansList;

        public ViewLoansViewModel(InvestmentFundModel investmentFundModel)
        {
            _investmentFundModel = investmentFundModel;
            UpdateContent();
        }

        public ICommand UpdateDataCommand { get; }

        public ObservableCollection<ViewLoanItemViewModel> LoansList
        {
            get => _loansList;
            set => SetProperty(ref _loansList, value);
        }

        public async void GetLoans()
        {
            LoansList = new ObservableCollection<ViewLoanItemViewModel>();
            var loansDic = await _investmentFundModel.GetAllLoans();
            foreach (var people in loansDic)
            foreach (var account in people.Value)
            foreach (var loans in account.Value)
            {
                var loanItem = new ViewLoanItemViewModel(loans.Id,
                    loans.Amount.ToString(),
                    loans.InstallmentsCount,
                    people.Key.Name + " " + people.Key.LastName,
                    account.Key.Id,
                    loans.LendDate.ToString(CultureInfo.CurrentCulture),
                    loans.PersonalAccountNumber
                );
                LoansList.Add(loanItem);
            }
        }

        public sealed override void UpdateContent()
        {
            GetLoans();
        }
    }
}