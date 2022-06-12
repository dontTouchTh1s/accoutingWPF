using System;

namespace SSWheatAdmin.Models
{
    public class LoanTransactinosModel
    {
        public LoanTransactinosModel(long amount, ushort loanId, ushort accountId, string? personalAccountNumber)
        {
            Amount = amount;
            LoanId = loanId;
            Date = DateTime.Now;
            AccountId = accountId;
            PersonalAccountNumber = personalAccountNumber;
        }

        public LoanTransactinosModel(ushort id, long amount, DateTime date, ushort loanId, ushort accountId,
            string? personalAccountNumber)
        {
            Id = id;
            Amount = amount;
            Date = date;
            LoanId = loanId;
            AccountId = accountId;
            PersonalAccountNumber = personalAccountNumber;
        }


        public ushort Id { get; set; }
        public long Amount { get; init; }
        public DateTime Date { get; init; }
        public ushort LoanId { get; init; }
        public ushort AccountId { get; init; }
        public string? PersonalAccountNumber { get; init; }
    }
}