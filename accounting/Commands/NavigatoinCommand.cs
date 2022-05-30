using System;
using accounting.Store;

namespace accounting.Commands
{
    public class NavigatoinCommand : BaseCommand
    {
        private readonly Action _createViewModel;
        private readonly NavigationService _navigationStore;

        public NavigatoinCommand(NavigationService navigationStore, Action createViewModel)
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