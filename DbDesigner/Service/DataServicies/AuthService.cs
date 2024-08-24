using AutoMapper;
using Common.Domain;
using Common.Dtos.UserDtos;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.Auth;

namespace Service.DataServicies;

public class AuthService
{
    private readonly IRepository<User> _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _hasher;
    private readonly IMapper _mapper;

    public AuthService(
        IRepository<User> userRepository,
        IJwtProvider jwtProvider,
        IPasswordHasher hasher, 
        IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _hasher = hasher;
        _mapper = mapper;
    }

    public async Task Register(UserRegisterDto dto)
    {
        var user = _mapper.Map<User>(dto);
        var isNew = await IsNew(user.Email);
        
        if (isNew) throw new Exception("This user is already exist");
        
        var passwordHash = _hasher.Generate(dto.Password);
        user.PasswordHash = passwordHash;

        await _userRepository.AddAsync(user);
    }
    
    public async Task<string> GoogleLogin(string name, string email)
    {
        var isNew = await IsNew(email);

        if (isNew)
        {
            var registerDto = new UserRegisterDto { Name = name, Email = email };
            await Register(registerDto);
        }
        var loginDto = new UserLoginDto { Email = email };
        var token = await Login(loginDto);    
        return token;
    }
    
    public async Task<string> Login(UserLoginDto dto)
    {
        var user = await _userRepository.Query().FirstOrDefaultAsync(e => e.Email == dto.Email);
        if (user == null) throw new Exception("This user does not exist");

        var passwordIsCorrect = _hasher.Verify(dto.Password, user.PasswordHash);
        if (!passwordIsCorrect) throw new Exception("Password is not correct");

        var token = _jwtProvider.GenerateToken(user);
        return token;
    }

    private async Task<bool> IsNew(string email) => await _userRepository.Query().AllAsync(e => e.Email != email);
}