using System;

namespace accounting.Exceptions
{
    public class NotEnoughFundAvailableBalance : Exception
    {
        public long AvailableBalance;

        public NotEnoughFundAvailableBalance(long availableBalance) : base("Not enough available balance in fund")
        {
            AvailableBalance = availableBalance;
        }
    }
}