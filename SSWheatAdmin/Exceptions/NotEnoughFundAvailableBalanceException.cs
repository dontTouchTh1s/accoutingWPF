using System;

namespace SSWheatAdmin.Exceptions
{
    public class NotEnoughFundAvailableBalanceException : Exception
    {
        public readonly ulong AvailableBalance;

        public NotEnoughFundAvailableBalanceException(ulong availableBalance) : base(
            "Not enough available balance in fund")
        {
            AvailableBalance = availableBalance;
        }
    }
}