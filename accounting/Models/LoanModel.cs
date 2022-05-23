using System;

namespace accounting.Models
{
    public class LoanModel
    {
        public LoanModel(int accountId, int amount, int installmentsCount, string? personalAccountNumber)
        {
            Amount = amount;
            InstallmentsCount = installmentsCount;
            LendDate = DateTime.Now;
            AccountId = accountId;
            PersonalAccountNumber = personalAccountNumber;
        }

        public int Id { get; }
        public int Amount { get; }
        public int InstallmentsCount { get; }
        public DateTime LendDate { get; }
        public int AccountId { get; }
        public string? PersonalAccountNumber { get; }
    }
}