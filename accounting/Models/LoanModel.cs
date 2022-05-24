using System;

namespace accounting.Models
{
    public class LoanModel
    {
        public LoanModel(ushort accountId, ulong amount, byte installmentsCount, string? personalAccountNumber)
        {
            Amount = amount;
            InstallmentsCount = installmentsCount;
            LendDate = DateTime.Now;
            AccountId = accountId;
            PersonalAccountNumber = personalAccountNumber;
        }

        public ushort Id { get; }
        public ulong Amount { get; }
        public byte InstallmentsCount { get; }
        public DateTime LendDate { get; }
        public ushort AccountId { get; }
        public string? PersonalAccountNumber { get; }
    }
}