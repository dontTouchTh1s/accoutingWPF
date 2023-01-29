using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SSWheatAdmin
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CloseCommandHandler(object sender, EventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
         
                this.DragMove();
        }
    }
}