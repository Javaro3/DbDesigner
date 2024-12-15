using Common.Domain;
using Common.Dtos;
using Common.Dtos.User;

namespace Service.Interfaces.Infrastructure.DataServices;

public interface IUserDataService : IBaseDataService<User, UserDto, UserFilterDto, ComboboxDto>
{
    Task AddUser(UserRegisterDto dto);

    Task<UserDto?> GetCurrentUserAsync(string jwt);

    public Task AddUserWithRole(UserAddDto dto);
    
    List<ComboboxDto> GetForCombobox(int currentUserId);
}