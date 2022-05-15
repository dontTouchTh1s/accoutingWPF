using System.Collections.Generic;
using System.Threading.Tasks;
using accounting.DbContexts;
using accounting.DTOs;
using accounting.Services;
using accounting.ViewModels;

namespace accounting.Models
{
    public class InvestmentFundModel
    {
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;

        public InvestmentFundModel(string name, InvestmentFundDbContextFactory investmentFundDbContextFactory)
        {
            Name = name;
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
        }

        public string Name { get; }
        public int TotalCapital { get; }

        public async Task AddPeople(CreateAccountViewModel createAccountViewModel)
        {
            var people = new PeoplesModel(createAccountViewModel.NationalId!, createAccountViewModel.Name!,
                createAccountViewModel.LastName!, createAccountViewModel.FatherName!,
                createAccountViewModel.PersonalAccountNumber!, _investmentFundDbContextFactory, null);
            var peopleEntity = new DataBaseAddPeople(_investmentFundDbContextFactory);
            await peopleEntity.AddPeople(people);

            await people.AddAccount(people);
        }
    }
}