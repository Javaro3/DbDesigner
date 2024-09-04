using System.ComponentModel.DataAnnotations;
using Common.Validations;

namespace Common.Dtos.UserDtos;

public class UserLoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
 
    [Required]
    [PasswordValidation]
    public string Password { get; set; } = string.Empty;
}
