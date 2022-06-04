namespace accounting.ViewModels.ManageLoans
{
    public class ViewLoanItemViewModel
    {
        public ViewLoanItemViewModel(ushort id, string amount, byte instalmentCount,
            string ownerFullName, ushort accountId,
            string lendDate, string? personalAccountNumber)
        {
            Id = id;
            Amount = amount;
            InstalmentCount = instalmentCount;
            OwnerFullName = ownerFullName;
            AccountId = accountId;
            LendDate = lendDate;
            PersonalAccountNumber = personalAccountNumber ?? "وارد نشده";
        }

        public ushort Id { get; }
        public string Amount { get; }
        public byte InstalmentCount { get; }
        public string OwnerFullName { get; }
        public ushort AccountId { get; }
        public string LendDate { get; }
        public string PersonalAccountNumber { get; }
    }
}