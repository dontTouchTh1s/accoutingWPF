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

        public async Task<PeopleDTO> AddPeople(PeoplesModel people)
        {
            await using (var context = _investmentFundDbContextFactory.CreateDbContext())
            {
                var dtoConverter = new DTOConverter();
                var peopleDTO = dtoConverter.PeopleModelToDTO(people);
                context.Peoples.Add(peopleDTO);
                await context.SaveChangesAsync();
                return peopleDTO;
            }
        }
    }
}