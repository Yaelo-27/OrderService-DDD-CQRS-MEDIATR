using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public partial record PhoneNumber
    {
        private const int DefaultLength = 12; // Example: +34666666666 (including country code)
        private const string DefaultPattern = @"^\+\d{11}$"; // Example pattern for international phone numbers
        private PhoneNumber(string value) => Value = value; // Private constructor to prevent direct instantiation
        public static PhoneNumber? Create(string value){
           string normalizedValue = Normalize(value); // Normalize the input value to ensure consistent formatting for validation.
           if(string.IsNullOrEmpty(normalizedValue) || normalizedValue.Length != DefaultLength || !PhoneNumberRegex().IsMatch(normalizedValue))
           {
                return null; // Return null if the phone number is invalid, ensuring that only valid phone numbers are created.
           }
           return new PhoneNumber(normalizedValue);
        }
        public string Value {get; init; } //init only property to ensure immutability so that once a phone number is created, it cannot be changed.
        private static string Normalize(string value)
        {
            if(string.IsNullOrEmpty(value)) // In case the value is null or empty, return it as is to avoid unnecessary processing and potential errors when trying to normalize an invalid input.
            {
                return value;
            }
            return value.Replace(" ", "").Replace("-", ""); // Remove spaces and dashes to allow for flexible input formats while still validating against a consistent pattern.
        }

        [GeneratedRegex(DefaultPattern)] // Attribute to generate a regex method based on the provided pattern, improving performance by pre-compiling the regex.
        private static partial Regex PhoneNumberRegex(); // Partial method declaration for regex, allowing for separation of concerns and potential reuse of the regex pattern in other parts of the codebase.
    }
}