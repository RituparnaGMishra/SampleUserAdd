using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SampleUserAdd.CustomValidation
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("User is null , cannot validate");

            }
            else
            {
                var message = new List<string>();
                var password = (string)value;

                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasMiniMaxChars = new Regex(@".{8,15}");
                var hasLowerChar = new Regex(@"[a-z]+");
                var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

                if (!hasLowerChar.IsMatch(password))
                {
                    message.Add("Password should contain At least one lower case letter");

                }
                if (!hasUpperChar.IsMatch(password))
                {
                    message.Add("Password should contain At least one upper case letter");

                }
                if (!hasMiniMaxChars.IsMatch(password))
                {
                    message.Add("Password should not be less than 8 characters");

                }
                if (!hasNumber.IsMatch(password))
                {
                    message.Add("Password should contain At least one numeric value");

                }

                if (!hasSymbols.IsMatch(password))
                {
                    message.Add("Password should contain At least one special case characters");

                }


                if (message.Any())
                {
                    ErrorMessage = string.Join(",", message);

                    return new ValidationResult(ErrorMessage);
                }
                else
                {
                    return ValidationResult.Success;
                }

            }
        }
        }
}
