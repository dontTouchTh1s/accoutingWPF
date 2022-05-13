using System.Threading.Tasks;
using accounting.DbContexts;
using accounting.Services;

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

        public async Task<PeoplesModel> AddPeople(PeoplesModel people)
        {
            var peopleEntity = new DataBaseAddPeople(_investmentFundDbContextFactory);
            await peopleEntity.AddPeople(people);
            return people;
        }
    }
}