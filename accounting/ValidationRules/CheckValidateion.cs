using System;
using System.Text.RegularExpressions;

namespace accounting.ValidationRules
{
    public static class CheckValidation
    {
        public static bool Length(string? text, int length, bool isOptimal = false)
        {
            if (isOptimal && string.IsNullOrWhiteSpace(text)) return true;
            return (text ?? "").Length == length;
        }

        public static bool Length(string? text, int max, int min, bool isOptimal = false)
        {
            if (isOptimal && string.IsNullOrWhiteSpace(text)) return true;
            return (text ?? "").Length <= max && (text ?? "").Length >= min;
        }

        public static bool Required(string? text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }

        public static bool Alphabetical(string? text, bool isOptimal = false)
        {
            if (isOptimal && string.IsNullOrWhiteSpace(text)) return true;
            return Regex.IsMatch(text ?? "", @"^[؀-ۿ\s]+$");
        }

        public static bool Numerical(string? text, bool isOptimal = false)
        {
            if (isOptimal && string.IsNullOrWhiteSpace(text)) return true;
            return Regex.IsMatch(text ?? "", "\\d");
        }
    }
}