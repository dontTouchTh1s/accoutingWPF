using System.Threading.Tasks;
using accounting.Models;
using accounting.ViewModels;

namespace accounting.Commands
{
    public class MakeTransactionCommand : BaseAsyncCommand
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private readonly TransactionsViewModel _transactionViewModel;

        public MakeTransactionCommand(TransactionsViewModel transactionsViewModel,
            InvestmentFundModel transactionsModel)
        {
            _transactionViewModel = transactionsViewModel;
            _investmentFundModel = transactionsModel;
        }

        public override async Task ExecuteAsync()
        {
            await _investmentFundModel.MakeTransaction(_transactionViewModel);
        }
    }
}