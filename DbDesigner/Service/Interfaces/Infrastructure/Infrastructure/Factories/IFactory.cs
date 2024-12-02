namespace Service.Interfaces.Infrastructure.Infrastructure.Factories;

public interface IFactory<out T>
{
    T GetEntity(int entityId);
}