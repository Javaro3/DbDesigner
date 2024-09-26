using System.ComponentModel.DataAnnotations;
using Common.Attributes;

namespace Common.Dtos.UserDtos;

public class UserRegisterDto
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [PasswordValidation]
    public string Password { get; set; } = string.Empty;
}