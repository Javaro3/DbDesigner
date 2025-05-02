namespace DbDesigner.Domain.Options;

public class DatabaseOptions
{
    public DatabaseConfigOptions? PostgreSql { get; set; }

    public DatabaseConfigOptions? MySql { get; set; }

    public DatabaseConfigOptions? SqLite { get; set; }

    public DatabaseConfigOptions? MsSql { get; set; }
}