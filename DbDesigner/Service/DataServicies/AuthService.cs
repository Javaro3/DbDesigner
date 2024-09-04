using AutoMapper;
using Common.Domain;
using Common.Dtos.UserDtos;
using Common.Enums;
using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.Auth;

namespace Service.DataServicies;

public class AuthService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Role> _roleRepository;

    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _hasher;
    private readonly IMapper _mapper;

    public AuthService(
        IRepository<User> userRepository,
        IRepository<Role> roleRepository,
        IJwtProvider jwtProvider,
        IPasswordHasher hasher, 
        IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _jwtProvider = jwtProvider;
        _hasher = hasher;
        _mapper = mapper;
    }

    public async Task RegisterAsync(UserRegisterDto dto)
    {
        var user = _mapper.Map<User>(dto);
        var isNew = await IsNewAsync(user.Email);
        
        if (!isNew) throw new ValidationException(nameof(UserRegisterDto.Email), "This user is already exist");
        
        var passwordHash = _hasher.Generate(dto.Password);
        user.PasswordHash = passwordHash;
        
        var role = await _roleRepository.GetAsync((int)RoleEnum.User);
        user.Roles = new List<Role> {role};
        
        await _userRepository.AddAsync(user);
    }
    
    public async Task<string> GoogleLoginAsync(string name, string email)
    {
        var isNew = await IsNewAsync(email);

        if (isNew)
        {
            var registerDto = new UserRegisterDto { Name = name, Email = email };
            await RegisterAsync(registerDto);
        }
        var loginDto = new UserLoginDto { Email = email };
        var token = await LoginAsync(loginDto);    
        return token;
    }
    
    public async Task<string> LoginAsync(UserLoginDto dto)
    {
        var user = await _userRepository.Query().FirstOrDefaultAsync(e => e.Email == dto.Email);
        if (user == null) throw new ValidationException(nameof(UserLoginDto.Email), "This user does not exist");

        var passwordIsCorrect = _hasher.Verify(dto.Password, user.PasswordHash);
        if (!passwordIsCorrect) throw new ValidationException(nameof(UserLoginDto.Password), "Password is not correct");

        var token = _jwtProvider.GenerateToken(user);
        return token;
    }

    public async Task<HashSet<PermissionEnum>> GetUserPermissionsAsync(int userId)
    {
        var roles = await _userRepository.Query()
            .AsNoTracking()
            .Include(e => e.Roles)
            .ThenInclude(e => e.Permissions)
            .Where(e => e.Id == userId)
            .Select(e => e.Roles)
            .ToListAsync();

        return roles.SelectMany(e => e)
            .SelectMany(e => e.Permissions)
            .Select(e => (PermissionEnum)e.Id)
            .ToHashSet();
    }

    private async Task<bool> IsNewAsync(string email) => await _userRepository.Query().AllAsync(e => e.Email != email);
}