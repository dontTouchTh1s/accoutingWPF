namespace accounting.validation
{
    public enum ErrorTypes
    {
        Numeric,
        Length,
        MaxLength,
        MinLength,
        Alphabetical
    }

    public class Error
    {
        public Error(string text, ErrorTypes type)
        {
            Text = text;
            Type = type;
        }

        public string Text { get; set; }
        public ErrorTypes Type { get; set; }
    }
}