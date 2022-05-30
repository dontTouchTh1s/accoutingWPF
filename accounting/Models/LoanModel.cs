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

        public LoanModel(ushort id, ulong loanAmount, byte loanInstallmentsCount,DateTime lendDate, string? personalAccountNumber, ushort accountId)
        {
            Id = id;
            Amount = loanAmount;
            InstallmentsCount = loanInstallmentsCount;
            LendDate = lendDate;
            PersonalAccountNumber = personalAccountNumber;
            AccountId = accountId;
        }

        public ushort Id { get; }
        public ulong Amount { get; }
        public byte InstallmentsCount { get; }
        public DateTime LendDate { get; }
        public ushort AccountId { get; }
        public string? PersonalAccountNumber { get; }
    }
}