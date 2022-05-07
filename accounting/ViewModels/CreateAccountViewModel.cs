#nullable enable

using System.Windows.Input;
using accounting.Commands;

namespace accounting.ViewModels
{
    public class CreateAccountViewModel : BaseViewModel
    {
        private string? _name;
        private string? _fullName;

        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string? FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        public ICommand? CreateAccountCommand { get; }

        public CreateAccountViewModel()
        {
            CreateAccountCommand = new CreateAccountCommand();
        }
    }
}