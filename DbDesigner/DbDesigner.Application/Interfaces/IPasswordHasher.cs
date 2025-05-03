namespace DbDesigner.Application.Interfaces;

public interface IPasswordHasher
{
    public string Generate(string password);

    public bool Verify(string password, string passwordHash);
}