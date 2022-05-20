using System.Collections.Generic;
using System.Threading.Tasks;
using accounting.DataBase.Services;

namespace accounting.Models
{
    public class PeoplesModel
    {
        private readonly DataBasePeopleServices _dataBasePeopleServices;

        public PeoplesModel(string nationalId, string name, string lastName, string fatherName,
            string personalAccountNumber,
            List<AccountsModel>? accounts,
            DataBasePeopleServices dataBasePeopleServices)
        {
            FatherName = fatherName;
            LastName = lastName;
            Name = name;
            NationalId = nationalId;
            PersonalAccountNumber = personalAccountNumber;
            _dataBasePeopleServices = dataBasePeopleServices;
            Accounts = accounts;
        }

        public string FatherName { get; }
        public string LastName { get; }
        public string Name { get; }
        public string NationalId { get; }
        public string PersonalAccountNumber { get; }
        public List<AccountsModel>? Accounts { get; }

        public async Task AddAccount(PeoplesModel owner)
        {
            var account = new AccountsModel(_dataBasePeopleServices.DataBaseAccountsServices, owner);
            await _dataBasePeopleServices.DataBaseAccountsServices.CreateAccount(account);
        }
    }
}