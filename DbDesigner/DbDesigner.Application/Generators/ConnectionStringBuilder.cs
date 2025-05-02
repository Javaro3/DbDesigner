using DbDesigner.Domain.Enums;

namespace DbDesigner.Application.Generators;

public static class ConnectionStringBuilder
{
    private const string DataBaseNameKeyPostgreSql = "Database";
    private const string DataBaseNameKeyMySql = "Database";
    private const string DataBaseNameKeySqLite = "Data Source";
    private const string DataBaseNameKeyMsSql = "Database";
    
    public static string? GetConnectionStringCSharp(
        Dictionary<string, string> defaultConnectionStringParams,
        DataBaseEnum dataBase,
        string? dataBaseName = null)
    {
        switch (dataBase)
        {
            case DataBaseEnum.MySql:
            {
                var connectionStringParams = new Dictionary<string, string>(defaultConnectionStringParams)
                    {[DataBaseNameKeyMySql] = dataBaseName ?? defaultConnectionStringParams[DataBaseNameKeyMySql]};
                return string.Join("", connectionStringParams.Select(kv => $"{kv.Key}={kv.Value};"));        
            }
            case DataBaseEnum.PostgreSql:
            {
                var connectionStringParams = new Dictionary<string, string>(defaultConnectionStringParams)
                    {[DataBaseNameKeyPostgreSql] = dataBaseName ?? defaultConnectionStringParams[DataBaseNameKeyPostgreSql]};
                return string.Join("", connectionStringParams.Select(kv => $"{kv.Key}={kv.Value};"));        
            }
            case DataBaseEnum.SqLite:
            {
                var connectionStringParams = new Dictionary<string, string>(defaultConnectionStringParams)
                    {[DataBaseNameKeySqLite] = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"{defaultConnectionStringParams[DataBaseNameKeySqLite]}{dataBaseName}.db"))};
                return string.Join("", connectionStringParams.Select(kv => $"{kv.Key}={kv.Value};"));
            }
            case DataBaseEnum.MsSql:
            {
                var connectionStringParams = new Dictionary<string, string>(defaultConnectionStringParams)
                    {[DataBaseNameKeyMsSql] = dataBaseName ?? defaultConnectionStringParams[DataBaseNameKeyMsSql]};
                return string.Join("", connectionStringParams.Select(kv => $"{kv.Key}={kv.Value};"));        
            }
            default:
                return null;
        }
    }
    
    public static string? GetConnectionStringJavaScript(
        Dictionary<string, string> defaultConnectionStringParams,
        DataBaseEnum dataBase,
        string? dataBaseName = null)
    {
        switch (dataBase)
        {
            case DataBaseEnum.MySql:
            {
                var host = defaultConnectionStringParams["Server"];
                var user = defaultConnectionStringParams["User Id"];
                var password = defaultConnectionStringParams["Password"];
                var port = defaultConnectionStringParams["Port"];
                const string dialect = "mysql";
                return $"sequelize-auto -h {host} -d {dataBaseName} -u {user} -x {password} -p {port} --dialect {dialect} -o ./models --lang ts";        
            }
            case DataBaseEnum.PostgreSql:
            {
                var host = defaultConnectionStringParams["Host"];
                var user = defaultConnectionStringParams["Username"];
                var password = defaultConnectionStringParams["Password"];
                var port = defaultConnectionStringParams["Port"];
                const string dialect = "postgres";
                return $"sequelize-auto -h {host} -d {dataBaseName} -u {user} -x {password} -p {port} --dialect {dialect} -o ./models --lang ts";         
            }
            case DataBaseEnum.SqLite:
            {
                var databasePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"{defaultConnectionStringParams[DataBaseNameKeySqLite]}{dataBaseName}.db"));
                return $"sequelize-auto -o ./models -d {databasePath} -e sqlite -h {databasePath} --lang ts";        
            }
            case DataBaseEnum.MsSql:
            {
                var host = defaultConnectionStringParams["Server"];
                var user = defaultConnectionStringParams["User Id"];
                var password = defaultConnectionStringParams["Password"];
                const int port = 1433;
                const string dialect = "mssql";
                return $"sequelize-auto -h {host} -d {dataBaseName} -u {user} -x {password} -p {port} --dialect {dialect} -o ./models --lang ts";         
            }
            default:
                return null;
        }
    }

    public static string? GetConnectionStringPython(
        Dictionary<string, string> defaultConnectionStringParams,
        DataBaseEnum dataBase,
        string? dataBaseName = null)
    {
        switch (dataBase)
        {
            case DataBaseEnum.MySql:
            {
                var host = defaultConnectionStringParams["Server"];
                var user = defaultConnectionStringParams["User Id"];
                var password = defaultConnectionStringParams["Password"];
                return $"sqlacodegen mysql+mysqlconnector://{user}:{password}@{host}/{dataBaseName} --outfile models.py";        
            }
            case DataBaseEnum.PostgreSql:
            {
                var host = defaultConnectionStringParams["Host"];
                var user = defaultConnectionStringParams["Username"];
                var password = defaultConnectionStringParams["Password"];
                return $"sqlacodegen postgresql://{user}:{password}@{host}/{dataBaseName} --outfile models.py";         
            }
            case DataBaseEnum.MsSql:
            {
                var host = defaultConnectionStringParams["Server"];
                var user = defaultConnectionStringParams["User Id"];
                var password = defaultConnectionStringParams["Password"];
                return $"sqlacodegen mssql+pymssql://{user}:{password}@{host}/{dataBaseName} --outfile models.py";         
            }
            case DataBaseEnum.SqLite:
            {
                var databasePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"{defaultConnectionStringParams[DataBaseNameKeySqLite]}{dataBaseName}.db"));
                return $"sqlacodegen sqlite:///{databasePath} --outfile models.py";
            }
            default:
                return null;
        }
    }
}