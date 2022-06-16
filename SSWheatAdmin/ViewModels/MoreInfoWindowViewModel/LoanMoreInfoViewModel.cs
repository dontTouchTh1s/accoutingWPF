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

        private ObservableCollection<InstalmentLoanModel> _instalmentList;

        private string _nextInstalmentDate;

        private ulong _payedAmount;

        private byte _payedInstalmentCount;
        private ushort _id;

        public LoanMoreInfoViewModel(InvestmentFundModel investmentFundModel, ViewLoanItemViewModel loan,
            ushort windowId) : base(windowId)
        {
            _investmentFundModel = investmentFundModel;
            LoanId = $"({loan.Id})";
            _id = loan.Id;
            OwnerFullName = loan.OwnerFullName;
            LendDate = DateTime.Parse(loan.LendDate).Date.ToShortDateString();
            LoanAmount = loan.Amount;
            InstalmentCount = loan.InstalmentCount;
            InstalmentAmount =
                (ulong.Parse(LoanAmount, NumberStyles.Number, CultureInfo.CurrentCulture) / Convert.ToUInt64(InstalmentCount)).ToString("N0",
                    CultureInfo.CurrentCulture);
            OwnerAccountId = loan.AccountId;

            UpdateContent();
        }

        public byte InstalmentCount { get; }

        public ObservableCollection<InstalmentLoanModel> InstalmentList { get; set; }

        public string OwnerFullName { get; }

        public ushort OwnerAccountId { get; }

        public string LendDate { get; }

        public string LoanAmount { get; }

        public string InstalmentAmount { get; }

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

        public string LoanId { get; }

        private async void GetInstalments()
        {
            var instalments = await _investmentFundModel.GetLoanInstalments(_id);
            var instalmentLoanModels = instalments.ToList();
            //instalmentLoanModels.Select(inst => inst.Date.ToString(CultureInfo.CurrentCulture)).ToList();
            InstalmentList = new ObservableCollection<InstalmentLoanModel>(instalmentLoanModels);
            if (!instalmentLoanModels.Any())
                return;
            PayedAmount = InstalmentList.Select(inst => inst.Amount).Aggregate((a, b) => a + b);
            PayedInstalemtCount = Convert.ToByte(InstalmentList.Count());
            NextInstalmentDate = DateTime.Parse(LendDate).Date.AddMonths(PayedInstalemtCount + 1).ToShortDateString();
        }

        public sealed override void UpdateContent()
        {
            GetInstalments();
        }
    }
}