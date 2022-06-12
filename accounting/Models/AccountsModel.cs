using System;
using System.Threading.Tasks;
using accounting.DataBase.Services;

namespace accounting.Models
{
    public class AccountsModel
    {
        private readonly DataBaseAccountsServices _dataBaseAccountsServices;

        public AccountsModel(ushort id,
            ulong credit,
            ulong availableCredit,
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

        public AccountsModel(string ownerNationalId,
            ulong credit,
            DataBaseAccountsServices dataBaseAccountsServices)
        {
            _dataBaseAccountsServices = dataBaseAccountsServices;
            CreateDate = DateTime.Now;
            Credit = credit;
            AvailableCredit = credit;
            OwnerNationalId = ownerNationalId;
        }

        public ushort Id { get; }
        public ulong Credit { get; }
        public ulong AvailableCredit { get; }
        public DateTime CreateDate { get; }
        public string OwnerNationalId { get; }

        public async Task MakeTransactions(TransactionsModel transactionsModel)
        {
            await _dataBaseAccountsServices.MakeTransaction(transactionsModel);
        }
    }
}