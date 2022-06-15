namespace SSWheatAdmin.ViewModels.MoreInfoWindowViewModel
{
    public class BaseWindowsViewModel : BaseViewModel
    {
        public BaseWindowsViewModel(ushort windowId)
        {
            WindowId = windowId;
        }
        public ushort WindowId { get; }
    }
}