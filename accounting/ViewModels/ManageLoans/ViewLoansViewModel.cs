﻿using System.Collections.Generic;
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
        private string _serachText;
        private List<ViewLoanItemViewModel> _viewLoanItemViewModelsList = new();

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

        public string SearchText
        {
            get => _serachText;
            set
            {
                SetProperty(ref _serachText, value);
                FilterLoans(value);
            }
        }

        private void FilterLoans(string filterValue)
        {
            LoansList = new ObservableCollection<ViewLoanItemViewModel>(_viewLoanItemViewModelsList);
            foreach (var loanItem in _viewLoanItemViewModelsList.Where(loanItem =>
                         !loanItem.OwnerFullName.Contains(filterValue) &&
                         !loanItem.Amount.Contains(filterValue)))
                LoansList.Remove(loanItem);
        }

        private async void GetLoans()
        {
            LoansList = new ObservableCollection<ViewLoanItemViewModel>();
            var loansDic = await _investmentFundModel.GetAllLoans();
            foreach (var people in loansDic)
            foreach (var account in people.Value)
            foreach (var loan in account.Value)
            {
                var remainAmount = await _investmentFundModel.GetLoanRemainedAmount(loan.Id);
                var loanItem = new ViewLoanItemViewModel(loan.Id,
                    loan.Amount,
                    loan.InstallmentsCount,
                    people.Key.Name + " " + people.Key.LastName,
                    account.Key.Id,
                    loan.LendDate,
                    remainAmount,
                    loan.PersonalAccountNumber
                );
                LoansList.Add(loanItem);
            }

            LoansList = new ObservableCollection<ViewLoanItemViewModel>(LoansList.OrderBy(model =>
                model.OwnerFullName));
            _viewLoanItemViewModelsList = new List<ViewLoanItemViewModel>(LoansList);
        }

        public sealed override void UpdateContent()
        {
            GetLoans();
        }
    }
}