using System;

namespace accounting.Exceptions
{
    public class NotEnoughAvailableCreditException : Exception
    {
        public ulong AvailableCredit;

        public NotEnoughAvailableCreditException(ulong availableCredit) : base(
            $"Not Enough credit, available credit: {availableCredit}")
        {
            AvailableCredit = availableCredit;
        }
    }
}