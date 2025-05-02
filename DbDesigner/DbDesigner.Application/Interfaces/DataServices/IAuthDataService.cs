using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.User;

namespace DbDesigner.Application.Interfaces.DataServices;

public interface IAuthDataService
{
    Task RegisterAsync(UserRegisterDto dto);

    Task<TokenDto> LoginAsync(UserLoginDto dto);

    Task<TokenDto> GoogleLoginAsync(string name, string email);
}