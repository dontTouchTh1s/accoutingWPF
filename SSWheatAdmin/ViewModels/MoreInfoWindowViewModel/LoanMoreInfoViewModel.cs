using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using SSWheatAdmin.Models;
using SSWheatAdmin.ViewModels.ManageLoans;

namespace SSWheatAdmin.ViewModels.MoreInfoWindowViewModel
{
    internal class LoanMoreInfoViewModel : BaseWindowsViewModel
    {
        private readonly InvestmentFundModel _investmentFundModel;

        private string _instalmentAmount;

        private byte _instalmentCount;

        private ObservableCollection<InstalmentLoanModel> _instalmentList;

        private string _lendDate;

        private string _loanAmount;

        private string _nextInstalmentDate;

        private ushort _ownerAccountId;

        private string _ownerFullName;

        private ulong _payedAmount;

        private byte _payedInstalmentCount;

        public LoanMoreInfoViewModel(InvestmentFundModel investmentFundModel, ViewLoanItemViewModel loan,
            ushort windowId) : base(windowId)
        {
            _investmentFundModel = investmentFundModel;
            LoanId = loan.Id;
            OwnerFullName = loan.OwnerFullName;
            LendDate = loan.LendDate;
            InstalmentCount = loan.InstalmentCount;
            OwnerAccountId = loan.AccountId;
            LoanAmount = loan.Amount;
            UpdateContent();
        }

        public byte InstalmentCount
        {
            get => _instalmentCount;
            set => SetProperty(ref _instalmentCount, value);
        }

        public ObservableCollection<InstalmentLoanModel> InstalmentList
        {
            get => _instalmentList;
            private set => SetProperty(ref _instalmentList, value);
        }

        public string OwnerFullName
        {
            get => _ownerFullName;
            set => SetProperty(ref _ownerFullName, value);
        }

        public ushort OwnerAccountId
        {
            get => _ownerAccountId;
            set => SetProperty(ref _ownerAccountId, value);
        }

        public string LendDate
        {
            get => _lendDate;
            set => SetProperty(ref _lendDate, value);
        }

        public string LoanAmount
        {
            get => _loanAmount;
            set => SetProperty(ref _loanAmount, value);
        }

        public byte InstalemntCount
        {
            get => _instalmentCount;
            set => SetProperty(ref _instalmentCount, value);
        }

        public string InstalmentAmount
        {
            get => _instalmentAmount;
            set => SetProperty(ref _instalmentAmount, value);
        }

        public string NextInstalmentDate
        {
            get => _nextInstalmentDate;
            private set => SetProperty(ref _nextInstalmentDate, value);
        }

        public ulong PayedAmount
        {
            get => _payedAmount;
            private set => SetProperty(ref _payedAmount, value);
        }

        public byte PayedInstalemtCount
        {
            get => _payedInstalmentCount;
            private set => SetProperty(ref _payedInstalmentCount, value);
        }

        public ushort LoanId { get; }

        private async void GetInstalments()
        {
            var instalments = await _investmentFundModel.GetLoanInstalments(LoanId);
            var instalmentLoanModels = instalments.ToList();
            InstalmentList = new ObservableCollection<InstalmentLoanModel>(instalmentLoanModels);
            if (!instalmentLoanModels.Any())
                return;
            PayedAmount = InstalmentList.Select(inst => inst.Amount).Aggregate((a, b) => a + b);
            PayedInstalemtCount = Convert.ToByte(_instalmentList.Count());
            var date = DateTime.Parse(LendDate, CultureInfo.CurrentCulture);
            NextInstalmentDate = date.AddMonths(PayedInstalemtCount + 1).ToString(CultureInfo.CurrentCulture);
        }

        public sealed override void UpdateContent()
        {
            GetInstalments();
        }
    }
}