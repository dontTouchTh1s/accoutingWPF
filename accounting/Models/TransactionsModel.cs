using System;

namespace accounting.Models
{
    public class TransactionsModel
    {
        public TransactionsModel(int amount, int fundAccountId, string? personalAccountNumber)
        {
            Date = DateTime.Now;
            Amount = amount;
            FundAccountId = fundAccountId;
            PersonalAccountNumber = personalAccountNumber;
        }

        public int Id { get; }
        public int Amount { get; }
        public int FundAccountId { get; }
        public AccountsModel Account { get; }
        public DateTime Date { get; }
        public string? PersonalAccountNumber { get; }
    }
}