using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.User;
using Common.Enums;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.DataServices;
using Service.Interfaces.Infrastructure.Infrastructure;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Service.DataServices;

public class UserDataService : IUserDataService
{
    private readonly IUserRepository _userRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly IDataSourceHelper _dataSourceHelper;
    private readonly IBaseHelper<User, UserFilterDto> _userHelper;
    private readonly IPasswordHasher _hasher;
    private readonly IMapper _mapper;

    public UserDataService(
        IUserRepository userRepository,
        IRepository<Role> roleRepository,
        IPasswordHasher hasher,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<User, UserFilterDto> userHelper,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _hasher = hasher;
        _dataSourceHelper = dataSourceHelper;
        _userHelper = userHelper;
        _mapper = mapper;
    }

    public async Task AddUser(UserRegisterDto dto)
    {
        var user = _mapper.Map<User>(dto);

        user.PasswordHash = _hasher.Generate(dto.Password);
        user.CreatedOn = DateTime.UtcNow;
        
        var role = await _roleRepository.GetByIdAsync((int)RoleEnum.User) ?? new Role {Id = (int)RoleEnum.User};
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
        
        var user = await _userRepository.GetByIdAsync(userId);
        var dto = _mapper.Map<UserDto>(user);
        return dto;
    }

    public async Task<TransportDto<UserDto>> GetFilteredAsync(UserFilterDto filter)
    {
        var query = _userRepository.GetAll();
        query = _userHelper.ApplyFilter(query, filter);
        query = _userHelper.ApplySort(query, filter);
        
        var transportDto = await _dataSourceHelper.ApplyDataSource<User, UserDto>(query, filter, _mapper);
        return transportDto;
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var model = await _userRepository.GetByIdAsync(id);
        var dto = _mapper.Map<UserDto>(model);
        return dto;
    }

    public List<ComboboxDto> GetForCombobox()
    {
        var query = _userRepository.GetAll();
        var dtos = _mapper.Map<List<ComboboxDto>>(query);

        return dtos;
    }
    
    public List<ComboboxDto> GetForCombobox(int currentUserId)
    {
        var query = _userRepository.GetAll().Where(e => e.Id != currentUserId);
        var dtos = _mapper.Map<List<ComboboxDto>>(query);

        return dtos;
    }

    public async Task UpdateAsync(UserDto dto)
    {
        var model = _mapper.Map<User>(dto);
        await _userRepository.UpdateAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        var model = await _userRepository.GetByIdAsync(id);
        if (model != null)
            await _userRepository.DeleteAsync(model);
    }
}
    