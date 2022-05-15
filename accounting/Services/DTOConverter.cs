using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Documents;
using accounting.DTOs;
using accounting.Models;

namespace accounting.Services
{
    public class DTOConverter
    {
        public AccountDTO AccountModelToDTO(AccountsModel accountsModel)
        {
            CultureInfo faCulture = new CultureInfo("fa-IR");
            return new AccountDTO
            {
                Credit = accountsModel.Credit,
                CreateDate = accountsModel.CreateDate.ToString(faCulture),
                OwnerNationalId = accountsModel.OwnerNatinalId
            };
        }

        public PeopleDTO PeopleModelToDTO(PeoplesModel peoplesModel)
        {
            List<AccountDTO> accountdtoList = new List<AccountDTO>();
            if (peoplesModel.Accounts != null)
            {
                foreach (var acc in peoplesModel.Accounts)
                {
                    accountdtoList.Add(AccountModelToDTO(acc));
                }
            }

            return new PeopleDTO
            {
                NationalId = peoplesModel.NationalId,
                Name = peoplesModel.Name,
                LastName = peoplesModel.LastName,
                FatherName = peoplesModel.FatherName,
                PersonalAccountNumber = peoplesModel.PersonalAccountNumber,
                Accounts = accountdtoList
            };
        }
    }
}