using System.Globalization;

namespace SSWheatAdmin.ViewModels.ComboBoxItems
{
    public class LoanItemViewModel
    {
        public LoanItemViewModel(ushort id, ulong amount, byte instalmentCount, ulong remainedAmount)
        {
            Id = id;
            Amount = amount.ToString("N0", CultureInfo.CurrentCulture);
            InstalmentCount = instalmentCount;
            MinimumInstalmentAmount = (amount / instalmentCount).ToString("N0", CultureInfo.CurrentCulture);
            RemainedAmount = remainedAmount.ToString("N0", CultureInfo.CurrentCulture);
        }

        public ushort Id { get; }
        public string Amount { get; }
        public byte InstalmentCount { get; }
        public string MinimumInstalmentAmount { get; }
        public string RemainedAmount { get; }


        public override string ToString()
        {
            return Id.ToString();
        }
    }
}