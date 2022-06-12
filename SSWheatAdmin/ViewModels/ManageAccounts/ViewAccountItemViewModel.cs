using System.Globalization;

namespace SSWheatAdmin.ViewModels.ManageAccounts
{
    public class ViewAccountItemViewModel
    {
        public ViewAccountItemViewModel(ushort id, string ownerFullName, string ownerNationalId, ulong credit,
            ulong availableCredit, string createDate, string personalAccountNumber)
        {
            Id = id;
            OwnerFullName = ownerFullName;
            OwnerNationalId = ownerNationalId;
            Credit = credit.ToString("N0", CultureInfo.CurrentCulture);
            AvailableCredit = availableCredit.ToString("N0", CultureInfo.CurrentCulture);
            CreateDate = createDate;
            PersonalAccountNumber = personalAccountNumber;
        }


        public ushort Id { get; }
        public string OwnerFullName { get; }
        public string OwnerNationalId { get; }
        public string Credit { get; }
        public string AvailableCredit { get; }
        public string CreateDate { get; }
        public string PersonalAccountNumber { get; }
    }
}