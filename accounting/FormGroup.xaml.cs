using System.Windows.Controls;
using Validation = accounting.validation.Validation;

namespace accounting
{
    public partial class FormGroup : UserControl
    {
        public readonly Validation Validation;
        private bool _alphabetical;

        private int _length;
        private int _maxLength;
        private int _minLength;
        private bool _numeric;

        public FormGroup()
        {
            InitializeComponent();
            DataContext = this;
            Validation = new Validation(this);
        }

        public string Title { set; get; }

        public string CurrentErrorMessage { set; get; }
        public string LengthErrorMessage { get; set; }
        public string MaxLengthErrorMessage { get; set; }
        public string MinLengthErrorMessage { get; set; }
        public string NumericErrorMessage { get; set; }
        public string AlphabeticalErrorMessage { get; set; }

        public int Length
        {
            get => _length;
            set
            {
                _length = value;
                Validation.Length.Add(_length);
                Validation.Length.Error.Text = LengthErrorMessage;
            }
        }

        public int MaxLength
        {
            get => _maxLength;
            set
            {
                _maxLength = value;
                Validation.MaxLength.Add(_maxLength);
                Validation.Length.Error.Text = MaxLengthErrorMessage;
            }
        }

        public int MinLength
        {
            get => _minLength;
            set
            {
                _minLength = value;
                Validation.MinLength.Add(_minLength);
                Validation.Length.Error.Text = MinLengthErrorMessage;
            }
        }

        public bool Numeric
        {
            get => _numeric;
            set
            {
                _numeric = value;
                Validation.Numeric.Add();
                Validation.Length.Error.Text = NumericErrorMessage;
            }
        }

        public bool Alphabetical
        {
            get => _alphabetical;
            set
            {
                _alphabetical = value;
                Validation.Alphabetical.Add();
                Validation.Alphabetical.Error.Text = AlphabeticalErrorMessage;
            }
        }

        public TextBlock ErrorTextBlock => TbError;
        public TextBox ContentTextBox => TextBox;
    }
}