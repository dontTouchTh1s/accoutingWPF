using System.Windows.Input;
using accounting.Commands;
using accounting.Models;
using accounting.Store;

namespace accounting.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private BaseViewModel _currentViewModel;

        public MainViewModel(InvestmentFundModel investmentFundModel, NavigationService navigationService)
        {
            _navigationService = navigationService;
            CheckTabItemUpdateCommand = new CheckTabItemUpdateCommand(this, _navigationService);
            _navigationService.CurrentViewChanged += OnCurrentViewChanged;
            navigationService.Navigate(navigationService.SummeryViewModel);
        }

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public ICommand CheckTabItemUpdateCommand { get; }

        private void OnCurrentViewChanged()
        {
            CurrentViewModel = _navigationService.CurrentViewModel;
        }
    }
}