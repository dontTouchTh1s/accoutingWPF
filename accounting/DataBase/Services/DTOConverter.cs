using System.Globalization;
using accounting.DataBase.DTOs;
using accounting.Models;

namespace accounting.DataBase.Services
{
    public class DTOConverter
    {
        private readonly CultureInfo _faCulture = new("fa-IR");

        public AccountDTO AccountModelToDTO(AccountsModel accountsModel)
        {
            return new AccountDTO
            {
                Credit = accountsModel.Credit,
                CreateDate = accountsModel.CreateDate.ToString(_faCulture),
                OwnerNationalId = accountsModel.OwnerNationalId
            };
        }

        public AccountsModel AccountDTOToModel(AccountDTO accountDTO, DataBaseAccountsServices dataBaseAccountsServices)
        {
            return new AccountsModel(accountDTO.AccountId, accountDTO.OwnerNationalId, dataBaseAccountsServices);

        }

        public PeoplesDTO PeopleModelToDTO(PeoplesModel peoplesModel)
        {
            return new PeoplesDTO
            {
                NationalId = peoplesModel.NationalId,
                Name = peoplesModel.Name,
                LastName = peoplesModel.LastName,
                FatherName = peoplesModel.FatherName,
                PersonalAccountNumber = peoplesModel.PersonalAccountNumber
            };
        }

        public PeoplesModel PeopleDTOToModel(PeoplesDTO peoplesDTO, DataBasePeopleServices dataBasePeopleServices)
        {
            return new PeoplesModel
            (peoplesDTO.NationalId,
                peoplesDTO.Name,
                peoplesDTO.LastName,
                peoplesDTO.FatherName,
                peoplesDTO.PersonalAccountNumber,
                dataBasePeopleServices
            );
        }

        public TransactionsDTO TransactionsToDTO(TransactionsModel transactionsModel)
        {
            return new TransactionsDTO
            {
                Amount = transactionsModel.Amount,
                Date = transactionsModel.Date.ToString(_faCulture),
                PersonalAccountNumber = "0",
                AccountId = transactionsModel.FundAccountId
            };
        }
    }
}