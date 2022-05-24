using System;

namespace accounting.Exceptions
{
    public class NotEnoughCreditException : Exception
    {
        public int Credit;
        public int MaximumLoan;
        
        public NotEnoughCreditException(int credit) : base("Not enough credit")
        {
            Credit = credit;
            MaximumLoan = credit * 2;
        }
    }
}