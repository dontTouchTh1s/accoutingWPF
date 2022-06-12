using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using SSWheatAdmin.Models;

namespace SSWheatAdmin.ViewModels.ManageAccounts
{
    public class ViewAccountsViewModel : BaseViewModel
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private ObservableCollection<ViewAccountItemViewModel> _accountList;
        private string _searchText;
        private List<ViewAccountItemViewModel> _viewAcconutItemViewModel;

        public ViewAccountsViewModel(InvestmentFundModel investmentFundModel)
        {
            _investmentFundModel = investmentFundModel;
        }

        public ObservableCollection<ViewAccountItemViewModel> AccountList
        {
            get => _accountList;
            set => SetProperty(ref _accountList, value);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterAccounts(value);
            }
        }

        private void FilterAccounts(string filterValue)
        {
            AccountList = new ObservableCollection<ViewAccountItemViewModel>(_viewAcconutItemViewModel);
            foreach (var accountItem in _viewAcconutItemViewModel.Where(accountItem =>
                         !accountItem.OwnerFullName.Contains(filterValue) &&
                         !accountItem.OwnerNationalId.Contains(filterValue)))
                AccountList.Remove(accountItem);
        }

        private async void GetAccounts()
        {
            AccountList = new ObservableCollection<ViewAccountItemViewModel>();
            var accountsList = await _investmentFundModel.GetAllPeoplesAccounts();
            if (accountsList == null) return;
            foreach (var people in accountsList)
            foreach (var accountsModel in people.Value)
            {
                var item = new ViewAccountItemViewModel(
                    accountsModel.Id,
                    people.Key.Name + " " + people.Key.LastName,
                    people.Key.NationalId,
                    accountsModel.Credit,
                    accountsModel.AvailableCredit,
                    accountsModel.CreateDate.ToString(CultureInfo.CurrentCulture),
                    people.Key.PersonalAccountNumber ?? "وارد نشده"
                );
                AccountList.Add(item);
            }

            _viewAcconutItemViewModel = new List<ViewAccountItemViewModel>(AccountList);
        }

        public override async void UpdateContent()
        {
            GetAccounts();
        }
    }
}