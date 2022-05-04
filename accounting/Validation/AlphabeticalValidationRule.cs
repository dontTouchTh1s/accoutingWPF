using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace accounting.Validation
{
    public class AlphabeticalValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
        {
            return value == null
                ? ValidationResult.ValidResult
                : !Regex.IsMatch(value.ToString()!, @"^[a-zA-Z]+$")
                    ? new ValidationResult(false, "فقط حروف میتوانید وارد کنید..")
                    : ValidationResult.ValidResult;
        }
    }
}