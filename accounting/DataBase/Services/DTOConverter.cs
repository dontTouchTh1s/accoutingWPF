using System.Collections.Generic;
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

        public PeoplesDTO PeopleModelToDTO(PeoplesModel peoplesModel)
        {
            var accountsToList = new List<AccountDTO>();
            if (peoplesModel.Accounts != null)
                foreach (var acc in peoplesModel.Accounts)
                    accountsToList.Add(AccountModelToDTO(acc));

            return new PeoplesDTO
            {
                NationalId = peoplesModel.NationalId,
                Name = peoplesModel.Name,
                LastName = peoplesModel.LastName,
                FatherName = peoplesModel.FatherName,
                PersonalAccountNumber = peoplesModel.PersonalAccountNumber,
                Accounts = accountsToList
            };
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