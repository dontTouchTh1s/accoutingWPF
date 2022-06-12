using System;

namespace SSWheatAdmin.Exceptions
{
    public class NotEnoughCreditException : Exception
    {
        public ulong Credit;
        public ulong MaximumLoan;

        public NotEnoughCreditException(ulong credit) : base("Not enough credit")
        {
            Credit = credit;
            MaximumLoan = credit * 2;
        }
    }
}