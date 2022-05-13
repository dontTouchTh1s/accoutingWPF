using System;
using System.Threading.Tasks;
using accounting.DbContexts;
using accounting.DTOs;
using accounting.Models;

namespace accounting.Services
{
    public class DataBaseAddPeople
    {
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;

        public DataBaseAddPeople(InvestmentFundDbContextFactory investmentFundDbContextFactory)
        {
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
        }

        public async Task AddPeople(PeoplesModel people)
        {
            await using (var context = _investmentFundDbContextFactory.CreateDbContext())
            {
                var peopleDTO = CreatePeopleDTO(people);
                context.Peoples.Add(peopleDTO);
                await context.SaveChangesAsync();
            }
        }

        private PeopleDTO CreatePeopleDTO(PeoplesModel people)
        {
            return new PeopleDTO
            {
                NationalId = (people.NationalId),
                Name = people.Name,
                LastName = people.LastName,
                FatherName = people.FatherName,
                PersonalAccountNumber = people.PersonalAccountNumber
            };
        }
    }
}