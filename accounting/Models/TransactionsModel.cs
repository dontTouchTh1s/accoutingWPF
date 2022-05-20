using System;

namespace accounting.Models
{
    public class TransactionsModel
    {
        public TransactionsModel()
        {
            Date = DateTime.Now;
        }

        public int Id { get; set; }
        public int Amount { get; set; }
        public int FundAccountId { get; set; }
        public AccountsModel Account { get; set; }
        public DateTime Date { get; set; }
        public string? PersonalAccountNumber { get; set; }
    }
}