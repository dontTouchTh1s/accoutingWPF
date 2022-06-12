using System;

namespace SSWheatAdmin.Exceptions
{
    public class NationalIdExistException : Exception
    {
        public NationalIdExistException(string nationalId) : base(
            $"People with national id {nationalId} already exist")
        {
            NationalId = nationalId;
        }

        public string NationalId { get; }
    }
}