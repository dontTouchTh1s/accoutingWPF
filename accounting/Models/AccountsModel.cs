using System;
using System.Threading.Tasks;
using accounting.DataBase.Services;

namespace accounting.Models
{
    public class AccountsModel
    {
        private readonly DataBaseAccountsServices _dataBaseAccountsServices;

        public AccountsModel(int id,
            int credit,
            int availableCredit,
            DateTime createDate,
            string ownerNationalId,
            DataBaseAccountsServices dataBaseAccountsServices)
        {
            _dataBaseAccountsServices = dataBaseAccountsServices;
            Id = id;
            CreateDate = createDate;
            Credit = credit;
            AvailableCredit = availableCredit;
            OwnerNationalId = ownerNationalId;
        }

        public AccountsModel(int id,
            string ownerNationalId,
            DataBaseAccountsServices dataBaseAccountsServices)
        {
            _dataBaseAccountsServices = dataBaseAccountsServices;
            Id = id;
            CreateDate = DateTime.Now;
            Credit = 0;
            AvailableCredit = 0;
            OwnerNationalId = ownerNationalId;
        }

        public int Id { get; }
        public int Credit { get; }
        public int AvailableCredit { get; }
        public DateTime CreateDate { get; }
        public string OwnerNationalId { get; }

        public async Task MakeTransactions(TransactionsModel transactionsModel)
        {
            await _dataBaseAccountsServices.MakeTransaction(transactionsModel);
        }
    }
}