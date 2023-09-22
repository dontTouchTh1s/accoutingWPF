using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SSWheatAdmin.Views.MoreInfoWindow
{
    /// <summary>
    /// Interaction logic for AccountMoreInfo.xaml
    /// </summary>
    public partial class AccountMoreInfo : Window
    {
        public AccountMoreInfo()
        {
            InitializeComponent();
        }


        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
