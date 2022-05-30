using System;
using accounting.Store;

namespace accounting.Commands
{
    public class NavigatoinCommand : BaseCommand
    {
        private readonly Func<bool> _createViewModel;
        private readonly NavigationService _navigationStore;

        public NavigatoinCommand(NavigationService navigationStore, Func<bool> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public override void Execute(object? parameter)
        {
            _createViewModel();
        }
    }
}