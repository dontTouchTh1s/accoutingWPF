using System.Windows;
using System.Windows.Input;

namespace SSWheatAdmin.Views.MoreInfoWindow
{
    /// <summary>
    ///     Interaction logic for LoanMoreInfo.xaml
    /// </summary>
    public partial class LoanMoreInfo
    {
        public LoanMoreInfo()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = SystemParameters.PrimaryScreenWidth / 3;
            Height = SystemParameters.PrimaryScreenWidth / 100 * 50;
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}