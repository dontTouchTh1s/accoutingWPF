using System.Globalization;
using System.Windows.Controls;

namespace accounting.Validation
{
    public class LengthValidationRule : ValidationRule
    {
        public int? Max { set; get; }
        public int? Min { set; get; }
        public int? Length { set; get; }

        public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
        {
            if (value == null) return ValidationResult.ValidResult;

            var text = value.ToString()!;
            if (Max != null)
                if (text.Length > Max)
                    return new ValidationResult(false, "حداکثر کاراکتر مجاز " + Max + " است.");
            if (Min != null)
                if (text.Length < Min)
                    return new ValidationResult(false, "حداقل کاراکتر مجاز " + Min + " است.");
            if (Length != null)
                if (text.Length != Length)
                    return new ValidationResult(false, "لطفا " + Length + " کاراکتر وارد کنید.");
            return ValidationResult.ValidResult;
        }
    }
}