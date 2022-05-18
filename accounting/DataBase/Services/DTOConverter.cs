using System.Collections.Generic;
using System.Globalization;
using accounting.DataBase.DTOs;
using accounting.Models;

namespace accounting.DataBase.Services
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

        public PeoplesDTO PeopleModelToDTO(PeoplesModel peoplesModel)
        {
            List<AccountDTO> accountdtoList = new List<AccountDTO>();
            if (peoplesModel.Accounts != null)
            {
                foreach (var acc in peoplesModel.Accounts)
                {
                    accountdtoList.Add(AccountModelToDTO(acc));
                }
            }

            return new PeoplesDTO
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