namespace accounting.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            CreateAccountViewModel = new CreateAccountViewModel();
        }

        public CreateAccountViewModel CreateAccountViewModel { get; }
    }
}