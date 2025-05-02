using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Domain.Enums;
using DbDesigner.Domain.Options;
using MySqlConnector;

namespace DbDesigner.Application.Generators.DatabaseExecutors;

public class MySqlExecutor : BaseExecutor, IDatabaseExecutor
{
    public MySqlExecutor(DatabaseConfigOptions? options) : base(options)
    {
    }

    public async Task<string?> ValidateScriptAsync(string script, string dataBaseName)
    {
        MySqlConnection? connection = null;

        try
        {
            var connectionString = ConnectionStringBuilder.GetConnectionStringCSharp(Options!.ConnectionStringParams, DataBaseEnum.MySql, dataBaseName);
            connection = new MySqlConnection(connectionString);
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
                await MySqlConnection.ClearPoolAsync(connection);
                await connection.DisposeAsync();
            }
        }
    }

    public async Task<bool> CreateDatabaseAsync(string dataBaseName)
    {
        MySqlConnection? connection = null;
        try
        {
            var connectionString = ConnectionStringBuilder.GetConnectionStringCSharp(Options!.ConnectionStringParams, DataBaseEnum.MySql);
            connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var queryDrop = $"DROP DATABASE IF EXISTS `{dataBaseName}`;";
            await using (var command = new MySqlCommand(queryDrop, connection))
            {
                await command.ExecuteNonQueryAsync();
            }

            var queryCreate = $"CREATE DATABASE `{dataBaseName}`;";
            await using (var command = new MySqlCommand(queryCreate, connection))
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