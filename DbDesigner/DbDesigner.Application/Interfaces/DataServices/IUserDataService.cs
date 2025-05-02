using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.User;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Interfaces.DataServices;

public interface IUserDataService : IBaseDataService<User, UserDto, UserFilterDto, ComboboxDto>
{
    Task AddUser(UserRegisterDto dto);

    Task<UserDto?> GetCurrentUserAsync(string jwt);

    public Task AddUserWithRole(UserAddDto dto);
}