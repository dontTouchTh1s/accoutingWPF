using System.Collections.Generic;
using System.Threading.Tasks;
using accounting.DataBase.Services;

namespace accounting.Models
{
    public class PeoplesModel
    {
        private readonly DataBasePeopleServices _dataBasePeopleServices;

        public PeoplesModel(string nationalId, string name, string lastName, string fatherName,
            string? personalAccountNumber,
            DataBasePeopleServices dataBasePeopleServices)
        {
            FatherName = fatherName;
            LastName = lastName;
            Name = name;
            NationalId = nationalId;
            PersonalAccountNumber = personalAccountNumber;
            _dataBasePeopleServices = dataBasePeopleServices;
        }

        public string FatherName { get; }
        public string LastName { get; }
        public string Name { get; }
        public string NationalId { get; }
        public string? PersonalAccountNumber { get; }

        public async Task AddAccount(PeoplesModel owner, ulong credit)
        {
            var account =
                new AccountsModel(owner.NationalId, credit, _dataBasePeopleServices.DataBaseAccountsServices);
            await _dataBasePeopleServices.DataBaseAccountsServices.CreateAccount(account);
        }

        public async Task<IEnumerable<AccountsModel>> GetAllAccounts()
        {
            return await _dataBasePeopleServices.GetAllAccounts(NationalId);
        }
    }
}