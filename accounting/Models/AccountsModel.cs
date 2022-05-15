using System;
using System.Globalization;
using accounting.ViewModels;

namespace accounting.Models
{
    public class AccountsModel
    {
        public AccountsModel(PeoplesModel owner)
        {
            CreateDate = DateTime.Now;
            Credit = 0;
            OwnerNatinalId = owner.NationalId;
        }
        public int Credit { get; }
        public DateTime CreateDate { get; }
        public int AccountId { get; }
        public string OwnerNatinalId { get; }
    }
}