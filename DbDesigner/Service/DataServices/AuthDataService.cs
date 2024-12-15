using Common.Dtos;
using Common.Dtos.User;
using Common.Exceptions;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.DataServices;
using Service.Interfaces.Infrastructure.Infrastructure;

namespace Service.DataServices;

public class AuthDataService : IAuthDataService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _hasher;
    private readonly IUserRepository _userRepository;
    private readonly IUserDataService _userDataService;
    
    public AuthDataService(
        IJwtProvider jwtProvider,
        IPasswordHasher hasher, 
        IUserRepository userRepository,
        IUserDataService userDataService)
    {
        _jwtProvider = jwtProvider;
        _hasher = hasher;
        _userRepository = userRepository;
        _userDataService = userDataService;
    }

    public async Task RegisterAsync(UserRegisterDto dto)
    {
        var isNew = await _userRepository.IsNewAsync(dto.Email);
        if (!isNew)
        {
            throw new ExceptionModel("This user is already exist");
        }

        await _userDataService.AddUser(dto);
    }
    
    public async Task<TokenDto> LoginAsync(UserLoginDto dto)
    {
        var user = await _userRepository.GetUserByEmail(dto.Email);
        if (user == null)
        {
            throw new ExceptionModel("This user does not exist");
        }

        var passwordIsCorrect = _hasher.Verify(dto.Password, user.PasswordHash);
        if (!passwordIsCorrect)
        {
            throw new ExceptionModel("Password is not correct");
        }

        var token = _jwtProvider.GenerateToken(user);
        return new TokenDto { Token = token };
    }
    
    public async Task<TokenDto> GoogleLoginAsync(string name, string email)
    {
        var isNew = await _userRepository.IsNewAsync(email);

        if (isNew)
        {
            var registerDto = new UserRegisterDto { Name = name, Email = email };
            await RegisterAsync(registerDto);
        }
        var loginDto = new UserLoginDto { Email = email };
        var token = await LoginAsync(loginDto);    
        return token;
    }
}