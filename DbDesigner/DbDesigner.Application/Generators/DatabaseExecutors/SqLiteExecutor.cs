using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Domain.Enums;
using DbDesigner.Domain.Options;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace DbDesigner.Application.Generators.DatabaseExecutors;

public class SqLiteExecutor : BaseExecutor, IDatabaseExecutor
{
    public SqLiteExecutor(DatabaseConfigOptions? options) : base(options)
    {
        Batteries.Init();
    }

    public async Task<string?> ValidateScriptAsync(string script, string dataBaseName)
    {
        SqliteConnection? connection = null;

        try
        {
            var connectionString = ConnectionStringBuilder.GetConnectionStringCSharp(Options!.ConnectionStringParams, DataBaseEnum.SqLite, dataBaseName);
            connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = script;
            await command.ExecuteNonQueryAsync();

            return null;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        finally
        {
            if (connection is not null)
            {
                SqliteConnection.ClearPool(connection);
                await connection.DisposeAsync();
            }
        }
    }

    public async Task<bool> CreateDatabaseAsync(string dataBaseName)
    {
        try
        {
            var connectionString = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"{Options?.ConnectionStringParams.First().Value}{dataBaseName}.db"));
            if (File.Exists(connectionString))
            {
                File.Delete(connectionString); 
            }

            await using (var _ = File.Create(connectionString)) {}
            return true;
        }
        catch
        {
            return false;
        }
    }
}