using System;
using System.Windows.Input;

namespace accounting.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public virtual bool CanExecute(object? parameter)
        {
            return false;
        }

        public abstract void Execute(object? parameter);

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler? CanExecuteChanged;
    }
}