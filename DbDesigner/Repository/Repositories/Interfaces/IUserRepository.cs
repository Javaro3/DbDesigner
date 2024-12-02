using Common.Domain;

namespace Repository.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsNewAsync(string email);

    Task<User?> GetUserByEmail(string email);
}