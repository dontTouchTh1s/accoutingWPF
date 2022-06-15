using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SSWheatAdmin.ViewModels;
using SSWheatAdmin.ViewModels.MoreInfoWindowViewModel;
using SSWheatAdmin.Views.MoreInfoWindow;

namespace SSWheatAdmin.Services
{
    public class WindowsNavigationServices
    {
        private readonly Dictionary<ushort, Window> _openWindows = new Dictionary<ushort, Window>();
        public WindowsNavigationServices()
        {

        }

        public void OpenMoreInfoWindow(BaseWindowsViewModel viewModel)
        {
            if (_openWindows.ContainsKey(viewModel.WindowId))
            {
                var oldViewModel = _openWindows.First(x => x.Key == viewModel.WindowId);
                oldViewModel.Value.Focus();
                return;
            }
            var win = new LoanMoreInfo
            {
                DataContext = viewModel
            };
            win.Show();
            _openWindows.Add(viewModel.WindowId, win);
        }
    }
}