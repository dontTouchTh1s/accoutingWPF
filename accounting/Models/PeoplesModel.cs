using System.Collections.Generic;
using System.Threading.Tasks;
using accounting.DbContexts;
using accounting.Services;

namespace accounting.Models
{
    public class PeoplesModel
    {
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;

        public PeoplesModel(string nationalId, string name, string lastName, string fatherName,
            string personalAccountNumber, InvestmentFundDbContextFactory investmentFundDbContextFactory,
            List<AccountsModel>? accounts)
        {
            FatherName = fatherName;
            LastName = lastName;
            Name = name;
            NationalId = nationalId;
            PersonalAccountNumber = personalAccountNumber;
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
            Accounts = accounts;
        }

        public string FatherName { get; }
        public string LastName { get; }
        public string Name { get; }
        public string NationalId { get; }
        public string PersonalAccountNumber { get; }
        public List<AccountsModel>? Accounts { get; }

        public async Task AddAccount(PeoplesModel owner)
        {
            var account = new AccountsModel(owner);
            var accountEntity = new DataBaseCreateAccount(_investmentFundDbContextFactory);
            await accountEntity.CreateAccount(account);
        }
    }
}