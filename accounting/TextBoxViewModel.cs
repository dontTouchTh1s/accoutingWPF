#nullable enable
namespace accounting
{
    public class TextBoxViewModel
    {
        private string? _name;

        public string? Name
        {
            get => _name;
            set => _name = value;
        }
    }
}