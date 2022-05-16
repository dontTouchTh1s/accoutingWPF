using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using Color = System.Drawing.Color;

namespace accounting.ViewModels.Dialogs
{
    public class MessageDialogViewModel : BaseViewModel
    {
        private string _messageContent = null!;
        private PackIconKind _messageIcon;
        private SolidColorBrush _messageIconColor;


        public MessageDialogViewModel(string name, PackIconKind messageIcon, SolidColorBrush messageIconColor)
        {
            MessageContent = name;
            MessageIcon = messageIcon;
            MessageIconColor = messageIconColor;
        }

        public string MessageContent
        {
            get => _messageContent;
            set => SetProperty(ref _messageContent, value);
        }

        public PackIconKind MessageIcon
        {
            get => _messageIcon;
            set => SetProperty(ref _messageIcon, value);
        }

        public SolidColorBrush MessageIconColor
        {
            get => _messageIconColor;
            set => SetProperty(ref _messageIconColor, value);
        }
    }
}