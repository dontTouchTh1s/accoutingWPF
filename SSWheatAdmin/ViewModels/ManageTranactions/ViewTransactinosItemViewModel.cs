using System;
using System.Globalization;

namespace SSWheatAdmin.ViewModels.ManageTranactions
{
    public class ViewTransactinosItemViewModel
    {
        public ViewTransactinosItemViewModel(ushort id, string ownerFullName, long amount, ushort accountId,
            DateTime date, string? personalAccountNumber)
        {
            Id = id;
            OwnerFullName = ownerFullName;
            Amount = Math.Abs(amount).ToString("N0", CultureInfo.CurrentCulture);
            AccountId = accountId;
            Date = date.ToString(CultureInfo.CurrentCulture);
            PersonalAccountNumber = personalAccountNumber ?? "وارد نشده";
            Type = amount < 0 ? "برداشت" : "واریز";
        }

        public ushort Id { get; }
        public string OwnerFullName { get; }
        public string Amount { get; }
        public ushort AccountId { get; }
        public string Date { get; }
        public string Type { get; }
        public string PersonalAccountNumber { get; }
    }
}