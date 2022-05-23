using System.Threading.Tasks;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task Animation_Label()
        {
            //await Task.Delay(3000);
            //var animation = new DoubleAnimation(0, TimeSpan.FromSeconds(2));
            //LblMessage.BeginAnimation(OpacityProperty, animation);
        }

        private async void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataContext =
                new MessageDialogViewModel("hello world", PackIconKind.About, new SolidColorBrush(Colors.Aqua));
            await DialogHost.Show(dataContext, "rootDialog");
        }
    }
}