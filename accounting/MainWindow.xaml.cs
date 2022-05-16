using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using accounting.ViewModels.Dialogs;
using MaterialDesignThemes.Wpf;

namespace accounting
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string? _amount;
        private MsSql _sql = null!;

        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        ///     Connect to data base when form loaded
        /// </summary>
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void textBox_Copy2_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private async Task Animation_Label()
        {
            //await Task.Delay(3000);
            //var animation = new DoubleAnimation(0, TimeSpan.FromSeconds(2));
            //LblMessage.BeginAnimation(OpacityProperty, animation);
        }

        private async void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataContext = new MessageDialogViewModel("hello world", PackIconKind.About, new SolidColorBrush(Colors.Aqua));
            await DialogHost.Show(dataContext, "rootDialog");
        }
    }
}