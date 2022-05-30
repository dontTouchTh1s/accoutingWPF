using System.Windows;
using System.Windows.Input;

namespace accounting.Commands.CurrencyComboBoxCommands
{
    public class CreditPreviewKeyDownCommand : BaseCommand
    {
        public override void Execute(object? parameter)
        {
            var args = (KeyEventArgs)parameter!;
            if (args.Key is >= Key.A and <= Key.Z)
                args.Handled = true;
            switch (args.Key)
            {
                case Key.Left:
                    args.Handled = true;
                    SendKey((UIElement)args.Source, Key.Right);
                    break;
                case Key.Right:
                    args.Handled = true;
                    SendKey((UIElement)args.Source, Key.Left);
                    break;
            }
        }
        private void SendKey(UIElement sourceElement, Key keyToSend)
        {
            var args = new KeyEventArgs(InputManager.Current.PrimaryKeyboardDevice,
                PresentationSource.FromVisual(sourceElement), 0, keyToSend);
            args.RoutedEvent = Keyboard.KeyDownEvent;
            InputManager.Current.ProcessInput(args);
        }
    }
}