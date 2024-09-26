using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Common.Attributes;

public class PasswordValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var password = value as string;

        if (string.IsNullOrEmpty(password))
        {
            return new ValidationResult("Password cannot be empty");
        }

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMinimum8Chars = new Regex(@".{8,}");

        if (!hasMinimum8Chars.IsMatch(password))
            return new ValidationResult("Password must be at least 8 characters long");

        if (!hasUpperChar.IsMatch(password))
            return new ValidationResult("Password must contain at least one uppercase letter");

        if (!hasNumber.IsMatch(password))
            return new ValidationResult("Password must contain at least one number");
        
        return ValidationResult.Success;
    }
}