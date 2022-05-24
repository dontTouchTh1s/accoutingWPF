using System;

namespace accounting.Exceptions
{
    public class NotEnoughAvailableCreditException : Exception
    {
        public int AvailableCredit;
        public NotEnoughAvailableCreditException(int availableCredit) : base($"Not Enough credit, available credit: {availableCredit}")
        {
            AvailableCredit = availableCredit;
        }
    }
}