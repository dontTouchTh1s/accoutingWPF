using System.Globalization;

namespace accounting.ViewModels.ComboBoxItems
{
    public class LoanItemViewModel
    {
        public LoanItemViewModel(ushort id, ulong amount, byte instalmentCount)
        {
            Id = id;
            Amount = amount.ToString("N0", CultureInfo.CurrentCulture);
            InstalmentCount = instalmentCount;
            MinimumInstalmentAmount = (amount / instalmentCount).ToString("N0", CultureInfo.CurrentCulture);
        }

        public ushort Id { get; }
        public string Amount { get; }
        public byte InstalmentCount { get; }
        public string MinimumInstalmentAmount { get; }


        public override string ToString()
        {
            return Id.ToString();
        }
    }
}