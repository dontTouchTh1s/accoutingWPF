using System.Threading.Tasks;
using accounting.DataBase.DbContexts;
using accounting.DataBase.DTOs;
using accounting.Models;

namespace accounting.DataBase.Services
{
    public class DataBaseAddPeople
    {
        private readonly DTOConverter _dtoConverterService;
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;

        public DataBaseAddPeople(InvestmentFundDbContextFactory investmentFundDbContextFactory,
            DTOConverter dtoConverterService)
        {
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
            _dtoConverterService = dtoConverterService;
        }

        public async Task<PeoplesDTO> AddPeople(PeoplesModel people)
        {
            await using (var context = _investmentFundDbContextFactory.CreateDbContext())
            {
                var peopleDTO = _dtoConverterService.PeopleModelToDTO(people);
                context.Peoples.Add(peopleDTO);
                await context.SaveChangesAsync();
                return peopleDTO;
            }
        }
    }
}