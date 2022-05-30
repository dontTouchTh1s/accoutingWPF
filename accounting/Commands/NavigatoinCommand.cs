using System;
using accounting.Store;
using accounting.ViewModels;

namespace accounting.Commands
{
    public class NavigatoinCommand : BaseCommand
    {
        private readonly NavigationService _navigationService;

        public NavigatoinCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate((BaseViewModel)parameter!);
        }
    }
}