using System;

namespace accounting.Models
{
    public class AccountsModel
    {
        public AccountsModel(string name, string lastName, string fatherName, string nationalId,
            string personalAccountNumber)
        {
        }

        public int Credit { get; }
        public DateTime CreateDate { get; }
        public PeoplesModel Owner { get; }
    }
}