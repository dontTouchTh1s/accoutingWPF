using System;
using System.Globalization;

namespace SSWheatAdmin.ViewModels.ManageLoans
{
    public class ViewLoanItemViewModel
    {
        public ViewLoanItemViewModel(ushort id, ulong amount, byte instalmentCount,
            string ownerFullName, ushort accountId,
            DateTime lendDate, ulong remainedAmount, string? personalAccountNumber)
        {
            Id = id;
            Amount = amount.ToString("N0", CultureInfo.CurrentCulture);
            InstalmentCount = instalmentCount;
            OwnerFullName = ownerFullName;
            AccountId = accountId;
            LendDate = lendDate.ToString(CultureInfo.CurrentCulture);
            RemainedAmount = remainedAmount.ToString("N0", CultureInfo.CurrentCulture);
            PersonalAccountNumber = personalAccountNumber ?? "وارد نشده";
            MinimumInstalmentAmount = (amount / instalmentCount).ToString("N0", CultureInfo.CurrentCulture);
        }

        public ushort Id { get; }
        public string OwnerFullName { get; }
        public string Amount { get; }
        public byte InstalmentCount { get; }
        public ushort AccountId { get; }
        public string LendDate { get; }
        public string RemainedAmount { get; }
        public string PersonalAccountNumber { get; }
        public string MinimumInstalmentAmount { get; }
    }
}