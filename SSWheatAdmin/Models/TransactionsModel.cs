using System;

namespace SSWheatAdmin.Models
{
    public class TransactionsModel
    {
        public TransactionsModel(ushort id, long amount, ushort fundAccountId, DateTime date,
            string? personalAccountNumber)
        {
            Id = id;
            Amount = amount;
            FundAccountId = fundAccountId;
            Date = date;
            PersonalAccountNumber = personalAccountNumber;
        }

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
        public DateTime Date { get; }
        public string? PersonalAccountNumber { get; }
    }
}