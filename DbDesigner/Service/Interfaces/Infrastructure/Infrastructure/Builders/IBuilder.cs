namespace Service.Interfaces.Infrastructure.Infrastructure.Builders;

public interface IBuilder<out TResult>
{
    TResult Generate();
}