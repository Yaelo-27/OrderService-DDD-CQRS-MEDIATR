using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public partial record Contact
    {
       private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        private Contact(string name, string email, PhoneNumber phoneNumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        public static Contact? Create(string name, string email, PhoneNumber phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || phoneNumber == null || !EmailRegex().IsMatch(email))
            {
                return null; // Return null if any of the required fields are invalid, ensuring that only valid contacts are created.
            }
            
            return new Contact(name, email, phoneNumber);
        }
        public string Name { get; init; }
        public string Email { get; init; }
        public PhoneNumber PhoneNumber { get; init; }

        [GeneratedRegex(EmailPattern)] //Source generator attribute to create a regex method for validating email addresses, improving performance by pre-compiling the regex and ensuring that email validation is consistent across the application.
        private static partial Regex EmailRegex();
    }
}