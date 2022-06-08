using System.Windows.Controls;
using System.Windows.Input;

namespace accounting.Views.ManageTransactions
{
    /// <summary>
    ///     Interaction logic for TransactionView.xaml
    /// </summary>
    public partial class TransactionView : UserControl
    {
        public TransactionView()
        {
            InitializeComponent();
        }

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            comboBox.IsDropDownOpen = true;
        }
    }
}