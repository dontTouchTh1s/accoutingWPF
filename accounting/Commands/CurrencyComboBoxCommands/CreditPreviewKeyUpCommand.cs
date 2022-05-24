using System.Windows;
using System.Windows.Input;
using accounting.ViewModels;

namespace accounting.Commands
{
    public class CreditPreviewKeyUpCommand : BaseCommand
    {
        public override void Execute(object? parameter)
        {
            var args = (KeyEventArgs)parameter!;
            if (args.Key is >= Key.A and <= Key.Z)
                args.Handled = true;
        }
    }
}