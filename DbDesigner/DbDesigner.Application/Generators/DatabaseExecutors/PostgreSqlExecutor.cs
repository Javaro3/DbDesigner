using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Domain.Enums;
using DbDesigner.Domain.Options;
using Npgsql;

namespace DbDesigner.Application.Generators.DatabaseExecutors;

public class PostgreSqlExecutor : BaseExecutor, IDatabaseExecutor
{
    public PostgreSqlExecutor(DatabaseConfigOptions? options) : base(options)
    {
    }

    public async Task<string?> ValidateScriptAsync(string script, string dataBaseName)
    {
        NpgsqlConnection? connection = null;

        try
        {
            var connectionString = ConnectionStringBuilder.GetConnectionStringCSharp(Options!.ConnectionStringParams, DataBaseEnum.PostgreSql, dataBaseName);
            connection = new NpgsqlConnection(connectionString);
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
                NpgsqlConnection.ClearPool(connection);
                await connection.DisposeAsync();
            }
        }
    }

    public async Task<bool> CreateDatabaseAsync(string dataBaseName)
    {
        NpgsqlConnection? connection = null;
        try
        {
            var connectionString = ConnectionStringBuilder.GetConnectionStringCSharp(Options!.ConnectionStringParams, DataBaseEnum.PostgreSql);
            connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            var queryDrop = $"DROP DATABASE IF EXISTS \"{dataBaseName}\";";
            await using (var command = new NpgsqlCommand(queryDrop, connection))
            {
                await command.ExecuteNonQueryAsync();
            }

            var queryCreate = $"CREATE DATABASE \"{dataBaseName}\";";
            await using (var command = new NpgsqlCommand(queryCreate, connection))
            {
                await command.ExecuteNonQueryAsync();
            }

            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            if (connection != null)
            {
                await connection.CloseAsync();
            }
        }
    }
}