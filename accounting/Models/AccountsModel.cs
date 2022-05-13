using System;

namespace accounting.Models
{
    public class AccountsModel
    {
        public AccountsModel(PeoplesModel owner)
        {
            Owner = owner;
            CreateDate = DateTime.Now;
            Credit = 0;
        }


        public int Credit { get; }
        public DateTime CreateDate { get; }
        public PeoplesModel Owner { get; }
    }
}