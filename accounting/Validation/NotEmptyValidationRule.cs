using System.Globalization;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace accounting.Validation
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
        {
            return value == null ? ValidationResult.ValidResult :
            string.IsNullOrWhiteSpace((value).ToString())
                ? new ValidationResult(false, "فیلد را پر کنید.")
                : ValidationResult.ValidResult;
        }
    }
}