using System;

namespace SSWheatAdmin.Models
{
    public class InstalmentLoanModel
    {
        public InstalmentLoanModel(ushort id, ushort loanId, ulong amount, DateTime date)
        {
            Id = id;
            LoanId = loanId;
            Amount = amount;
            Date = date;
        }

        public InstalmentLoanModel(ushort loanId, ulong amount)
        {
            LoanId = loanId;
            Amount = amount;
            Date = DateTime.Now;
        }

        public ushort Id { get; }
        public ushort LoanId { get; }
        public ulong Amount { get; }
        public DateTime Date { get; }
    }
}