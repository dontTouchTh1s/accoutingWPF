using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SSWheatAdmin.ViewModels.MoreInfoWindowViewModel;
using SSWheatAdmin.Views.MoreInfoWindow;

namespace SSWheatAdmin.Services
{
    public class WindowsNavigationServices
    {
        private readonly Dictionary<ushort, Window> _openWindows = new();

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
            win.Closed += OnWindowsClose;
            _openWindows.Add(viewModel.WindowId, win);
        }

        public void CloseWindows(ushort windowId)
        {
            if (!_openWindows.ContainsKey(windowId)) return;
            var oldViewModel = _openWindows.First(x => x.Key == windowId);
            oldViewModel.Value.Close();
            _openWindows.Remove(oldViewModel.Key);
        }

        private void OnWindowsClose(object? sender, EventArgs eventArgs)
        {
            _openWindows.Remove(_openWindows.First(win => win.Value == sender).Key);
        }

    }
}