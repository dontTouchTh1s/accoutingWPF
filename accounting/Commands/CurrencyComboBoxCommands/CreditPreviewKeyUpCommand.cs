using System.Windows.Input;

namespace accounting.Commands.CurrencyComboBoxCommands
{
    public class CreditPreviewKeyUpCommand : BaseCommand
    {
        public override void Execute(object? parameter)
        {
            var args = (KeyEventArgs)parameter!;
            
        }
    }
}