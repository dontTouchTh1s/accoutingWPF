using System.Threading.Tasks;
using accounting.Models;
using accounting.ViewModels;

namespace accounting.Commands
{
    public class AccountIdTextChangedCommand : BaseAsyncCommand
    {
        public AccountIdTextChangedCommand(TransactionsViewModel transactionsViewModel,
            InvestmentFundModel investmentFundModel)
        {
        }

        public override Task ExecuteAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}