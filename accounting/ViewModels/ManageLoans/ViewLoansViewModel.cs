using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
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
            LoansList = new ObservableCollection<ViewLoanItemViewModel>(LoansList.OrderBy(model => model.OwnerFullName));
        }

        public sealed override void UpdateContent()
        {
            GetLoans();
        }
    }
}