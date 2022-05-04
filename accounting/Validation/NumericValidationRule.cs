using System.Globalization;
using System.Windows.Controls;

namespace accounting.Validation
{
    public class NumericValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
        {
            return value == null
                ? ValidationResult.ValidResult
                : !int.TryParse(value.ToString(), out _)
                    ? new ValidationResult(false, "فقط عدد میتوانید وارد کنید.")
                    : ValidationResult.ValidResult;
        }
    }
}