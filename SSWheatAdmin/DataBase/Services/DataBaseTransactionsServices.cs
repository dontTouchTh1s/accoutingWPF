using SSWheatAdmin.DataBase.DbContexts;

namespace SSWheatAdmin.DataBase.Services
{
    public class DataBaseTransactionsServices
    {
        private readonly DTOConverter _dtoConverterService;
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;

        public DataBaseTransactionsServices(InvestmentFundDbContextFactory investmentFundDbContextFactory,
            DTOConverter dtoConverterService)
        {
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
            _dtoConverterService = dtoConverterService;
        }
    }
}