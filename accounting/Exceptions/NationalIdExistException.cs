using System;

namespace accounting.Exceptions
{
    public class NationalIdExistException : Exception
    {
        public NationalIdExistException()
        {
        }

        public NationalIdExistException(string nationalId) : base(
            $"People with national id {nationalId} already exist")
        {
            NationalId = nationalId;
        }

        public string NationalId { get; }
    }
}