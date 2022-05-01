using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace accounting.validation
{
    public class Validation
    {
        public readonly TextBlock ErrorTextBlock;
        public List<Error> Errors = new();
        public bool IsValid;
        public TextBox TextBox;
        public FormGroup FormGroup;

        public Validation(FormGroup formGroup)
        {
            FormGroup = formGroup;
            Length = new Length(this);
            MaxLength = new MaxLength(this);
            MinLength = new MinLength(this);
            Numeric = new Numeric(this);
            Alphabetical = new Alphabetical(this);
            TextBox = formGroup.TextBox;
            ErrorTextBlock = formGroup.ErrorTextBlock;
        }

        public Length Length { get; }
        public MaxLength MaxLength { get; }
        public MinLength MinLength { get; }
        public Numeric Numeric { get; }
        public Alphabetical Alphabetical { get; }
    }

    public class ValidationType
    {
        protected readonly Validation Parent;
        public Error Error = new("", ErrorTypes.Numeric);
        public int ValidateValue;

        protected ValidationType(Validation parent)
        {
            Parent = parent;
        }

        protected void AddError(Error error)
        {
            Parent.IsValid = false;
            if (!Parent.Errors.Contains(error))
            {
                Parent.Errors.Add(error);
                Parent.FormGroup.CurrentErrorMessage = error.Text;
            }
        }

        protected void RemoveError(Error error)
        {
            Parent.Errors.Remove(error);
            if (Parent.Errors.Count == 0)
                Parent.FormGroup.CurrentErrorMessage = "";
        }
    }

    public class Numeric : ValidationType
    {
        public Numeric(Validation parent) : base(parent)
        {
            Error.Text = "لطفا فقط عدد وارد کنید.";
        }

        public void Add()
        {
            Parent.TextBox.KeyUp += ValidateByNumeric;
        }

        public void Remove()
        {
            Parent.TextBox.KeyUp -= ValidateByNumeric;
        }

        private void ValidateByNumeric(object sender, EventArgs e)
        {
            if (int.TryParse(Parent.TextBox.Text, out var n))
                RemoveError(Error);
            else
                AddError(Error);
        }
    }

    public class MinLength : ValidationType
    {
        public MinLength(Validation parent) : base(parent)
        {
            Error.Type = ErrorTypes.MinLength;
        }

        public void Add(int length)
        {
            ValidateValue = length;
            Parent.TextBox.KeyUp += ByMaxLength;
        }

        public void Remove()
        {
            Parent.TextBox.KeyUp -= ByMaxLength;
        }

        private void ByMaxLength(object sender, EventArgs e)
        {
            if (Parent.TextBox.Text.Length < ValidateValue)
                RemoveError(Error);
            else
                AddError(Error);
        }
    }

    public class MaxLength : ValidationType
    {
        public MaxLength(Validation parent) : base(parent)
        {
            Error.Type = ErrorTypes.MaxLength;
        }

        public void Add(int maxLength)
        {
            ValidateValue = maxLength;
            Parent.TextBox.KeyUp += ByMaxLength;
        }

        public void Remove()
        {
            Parent.TextBox.KeyUp -= ByMaxLength;
        }

        private void ByMaxLength(object sender, EventArgs e)
        {
            if (Parent.TextBox.Text.Length < ValidateValue)
                RemoveError(Error);
            else
                AddError(Error);
        }
    }

    public class Length : ValidationType
    {
        public Length(Validation parent) : base(parent)
        {
            Error.Type = ErrorTypes.Length;
        }

        public void Add(int length)
        {
            ValidateValue = length;
            Parent.TextBox.KeyUp += ValidateByLength;
        }

        public void Remove()
        {
            Parent.TextBox.KeyUp -= ValidateByLength;
        }

        private void ValidateByLength(object sender, EventArgs e)
        {
            if (Parent.TextBox.Text.Length == ValidateValue)
                RemoveError(Error);
            else
                AddError(Error);
        }
    }

    public class Alphabetical : ValidationType
    {
        public Alphabetical(Validation parent) : base(parent)
        {
            Error.Type = ErrorTypes.Alphabetical;
        }

        public void Add()
        {
            Parent.TextBox.KeyUp += ValidationAlphabetical;
        }

        public void Remove()
        {
            Parent.TextBox.KeyUp -= ValidationAlphabetical;
        }

        private void ValidationAlphabetical(object sender, EventArgs e)
        {
            if (Regex.IsMatch(Parent.TextBox.Text, @"^[a-zA-Z]+$"))
                RemoveError(Error);
            else
                AddError(Error);
        }
    }
}