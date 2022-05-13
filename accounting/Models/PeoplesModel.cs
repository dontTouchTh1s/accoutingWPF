namespace accounting.Models
{
    public class PeoplesModel
    {
        public PeoplesModel(string nationalId, string name, string lastName, string fatherName,
            string personalAccountNumber)
        {
            FatherName = fatherName;
            LastName = lastName;
            Name = name;
            NationalId = nationalId;
            PersonalAccountNumber = personalAccountNumber;
        }

        public string FatherName { get; }
        public string LastName { get; }
        public string Name { get; }
        public string NationalId { get; }
        public string PersonalAccountNumber { get; }

        public AccountsModel CreateAccount()
        {
            return new AccountsModel(this);
        }
    }
}