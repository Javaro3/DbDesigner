using Common.Domain;

namespace Service.Interfaces.Infrastructure.Infrastructure;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}