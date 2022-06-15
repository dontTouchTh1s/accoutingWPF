using System.Windows;

namespace SSWheatAdmin.Views.MoreInfoWindow
{
    /// <summary>
    /// Interaction logic for LoanMoreInfo.xaml
    /// </summary>
    public partial class LoanMoreInfo : Window
    {
        public LoanMoreInfo()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = SystemParameters.PrimaryScreenWidth / 3;
            Height = SystemParameters.PrimaryScreenWidth / 100 * 50;
        }
    }
}