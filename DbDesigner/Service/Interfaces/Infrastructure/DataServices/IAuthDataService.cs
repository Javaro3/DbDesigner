using Common.Dtos;
using Common.Dtos.User;

namespace Service.Interfaces.Infrastructure.DataServices;

public interface IAuthDataService
{
    Task RegisterAsync(UserRegisterDto dto);

    Task<TokenDto> LoginAsync(UserLoginDto dto);

    Task<TokenDto> GoogleLoginAsync(string name, string email);
}