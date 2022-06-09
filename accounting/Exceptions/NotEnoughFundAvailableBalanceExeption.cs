using System;

namespace accounting.Exceptions
{
    public class NotEnoughFundAvailableBalanceExeption : Exception
    {
        public readonly long AvailableBalance;

        public NotEnoughFundAvailableBalanceExeption(long availableBalance) : base(
            "Not enough available balance in fund")
        {
            AvailableBalance = availableBalance;
        }
    }
}