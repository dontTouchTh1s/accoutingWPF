using System;
using System.Windows.Input;
using accounting.Commands;
using accounting.Models;
using accounting.Store;

namespace accounting.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        public MainViewModel(InvestmentFundModel investmentFundModel, NavigationService navigationService)
        {
            _navigationService = navigationService;
            CheckTabItemUpdateCommand = new CheckTabItemUpdateCommand(this, _navigationService);
            _navigationService.CurrentViewChanged += OnCurrentViewChanged;
            navigationService.Navigate(navigationService.SummeryViewModel);
        }
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value);  }
        }

        private void OnCurrentViewChanged()
        {
            CurrentViewModel = _navigationService.CurrentViewModel;
        }

        public ICommand CheckTabItemUpdateCommand { get; }
    }
}