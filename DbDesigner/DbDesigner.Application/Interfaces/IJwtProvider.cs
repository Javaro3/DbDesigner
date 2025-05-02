using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Interfaces;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}