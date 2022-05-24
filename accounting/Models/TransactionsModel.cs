using System;

namespace accounting.Models
{
    public class TransactionsModel
    {
        public TransactionsModel(long amount, ushort fundAccountId, string? personalAccountNumber)
        {
            Date = DateTime.Now;
            Amount = amount;
            FundAccountId = fundAccountId;
            PersonalAccountNumber = personalAccountNumber;
        }

        public ushort Id { get; }
        public long Amount { get; }
        public ushort FundAccountId { get; }
        public AccountsModel? Account { get; }
        public DateTime Date { get; }
        public string? PersonalAccountNumber { get; }
    }
}