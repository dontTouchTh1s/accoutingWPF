namespace SSWheatAdmin.ViewModels.ComboBoxItems
{
    public class AccountsItemsViewModel
    {
        public AccountsItemsViewModel(ushort accountId, string accountOwnerFullName, string accountOwnerNationalId)
        {
            AccountId = accountId;
            AccountOwnerFullName = accountOwnerFullName;
            AccountOwnerNationalId = accountOwnerNationalId;
        }

        public ushort AccountId { get; }

        public string AccountOwnerFullName { get; }

        public string AccountOwnerNationalId { get; }

        public override string ToString()
        {
            return AccountId.ToString();
        }
    }
}