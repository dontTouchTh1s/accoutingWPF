using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using accounting.Models;

namespace accounting.ViewModels.ManageTranactions
{
    public class ViewTransactionsViewModel : BaseViewModel
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private string _serachText;
        private ObservableCollection<ViewTransactinosItemViewModel> _tranactionsList;
        private List<ViewTransactinosItemViewModel> _viewTransactinosItemViewModels = new();

        private byte _type;

        public ViewTransactionsViewModel(InvestmentFundModel investmentFundModel)
        {
            _investmentFundModel = investmentFundModel;
            UpdateContent();
        }

        public ObservableCollection<ViewTransactinosItemViewModel> TranactionsList
        {
            get => _tranactionsList;
            set => SetProperty(ref _tranactionsList, value);
        }

        public string SearchText
        {
            get => _serachText;
            set
            {
                SetProperty(ref _serachText, value);
                FilterTransactions(value);
            }
        }

        public byte Type
        {
            get => _type;
            set
            {
                SetProperty(ref _type, value);
                FilterTransactions(SearchText);
            }
        }

        private void FilterTransactions(string filterValue)
        {
            var transactionsType = _type switch
            {
                1 => "واریز",
                2 => "برداشت",
                _ => ""
            };

            TranactionsList = new ObservableCollection<ViewTransactinosItemViewModel>(_viewTransactinosItemViewModels);
            foreach (var transactinosItem in _viewTransactinosItemViewModels)
                if (_type != 0 && transactinosItem.Type != transactionsType)
                {
                    TranactionsList.Remove(transactinosItem);
                }
                else
                {
                    if (!transactinosItem.OwnerFullName.Contains(filterValue) &&
                        !transactinosItem.Amount.Contains(filterValue))
                        TranactionsList.Remove(transactinosItem);
                }
        }

        private async void GetLoans()
        {
            TranactionsList = new ObservableCollection<ViewTransactinosItemViewModel>();
            var transactinosList = await _investmentFundModel.GetAllTransactinos();
            foreach (var people in transactinosList)
            foreach (var account in people.Value)
            foreach (var transaction in account.Value)
            {
                var transactionItem = new ViewTransactinosItemViewModel(transaction.Id,
                    people.Key.Name + " " + people.Key.LastName,
                    transaction.Amount,
                    account.Key.Id,
                    transaction.Date,
                    transaction.PersonalAccountNumber
                );
                TranactionsList.Add(transactionItem);
            }

            TranactionsList = new ObservableCollection<ViewTransactinosItemViewModel>(TranactionsList.OrderBy(model =>
                model.OwnerFullName));
            _viewTransactinosItemViewModels = new List<ViewTransactinosItemViewModel>(TranactionsList);
        }

        public sealed override void UpdateContent()
        {
            GetLoans();
        }
    }
}