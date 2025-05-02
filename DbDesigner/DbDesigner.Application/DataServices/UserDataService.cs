using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.User;
using DbDesigner.Application.Interfaces;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Domain.Enums;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

public class UserDataService : BaseDataService<User, UserDto, UserFilterDto, ComboboxDto>, IUserDataService
{
    private readonly IUserRepository _userRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly IPasswordHasher _hasher;
    private readonly IMapper _mapper;

    public UserDataService(
        IUserRepository userRepository,
        IRepository<Role> roleRepository,
        IPasswordHasher hasher,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<User, UserFilterDto> userHelper,
        IMapper mapper) : base(userRepository, dataSourceHelper, userHelper, mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _hasher = hasher;
        _mapper = mapper;
    }

    public async Task AddUser(UserRegisterDto dto)
    {
        var user = _mapper.Map<User>(dto);

        user.PasswordHash = _hasher.Generate(dto.Password);
        user.CreatedOn = DateTime.UtcNow;
        
        var role = await _roleRepository.GetAsync((int)RoleEnum.User) ?? new Role {Id = (int)RoleEnum.User};
        user.Roles = new List<Role> { role };
        
        await _userRepository.AddAsync(user);
    }
    
    public async Task AddUserWithRole(UserAddDto dto)
    {
        var user = _mapper.Map<User>(dto);

        user.PasswordHash = _hasher.Generate(dto.Password);
        user.CreatedOn = DateTime.UtcNow;
        user.Roles = user.Roles.Select(e => new Role { Id = e.Id }).ToList();
        
        await _userRepository.AddAsync(user);
    }

    public async Task<UserDto?> GetCurrentUserAsync(string jwt)
    {
        var token = jwt["Bearer ".Length..].Trim();
        var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

        var userIdClaim = jwtToken?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim.Value, out var userId))
        {
            return null;
        }
        
        var user = await _userRepository.GetAsync(userId);
        var dto = _mapper.Map<UserDto>(user);
        return dto;
    }
}
    