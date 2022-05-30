namespace accounting.ViewModels.ComboBoxItems
{
    public class LoanItemViewModel
    {
        public LoanItemViewModel(ushort id, ulong amount, byte instalmentCount)
        {
            Id = id;
            Amount = amount;
            InstalmentCount = instalmentCount;
        }

        public ushort Id { get;}
        public ulong Amount { get;}
        public byte InstalmentCount { get;}

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}