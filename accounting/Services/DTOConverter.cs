using accounting.DTOs;
using accounting.Models;

namespace accounting.Services
{
    public class DTOConverter
    {
        public AccountDTO AccountModelToDTO(AccountsModel accountsModel)
        {
            PeopleDTO owner = PeopleModelToDTO(accountsModel.Owner);
            return new AccountDTO
            {
                AccountId = accountsModel.AccountId,
                Credit = accountsModel.Credit,
                Owner = owner,
                CreateDate = accountsModel.CreateDate
            };
        }

        public PeopleDTO PeopleModelToDTO(PeoplesModel peoplesModel)
        {
            return new PeopleDTO
            {
                NationalId = peoplesModel.NationalId,
                Name = peoplesModel.Name,
                LastName = peoplesModel.LastName,
                FatherName = peoplesModel.FatherName,
                PersonalAccountNumber = peoplesModel.PersonalAccountNumber
            };
        }
    }
}