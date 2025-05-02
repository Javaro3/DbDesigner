using DbDesigner.Domain.Domain;

namespace DbDesigner.Infrastructure.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsNewAsync(string email);

    Task<User?> GetUserByEmail(string email);
}