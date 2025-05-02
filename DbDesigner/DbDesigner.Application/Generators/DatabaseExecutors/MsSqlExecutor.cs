using System.Data;
using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Domain.Enums;
using DbDesigner.Domain.Options;
using Microsoft.Data.SqlClient;

namespace DbDesigner.Application.Generators.DatabaseExecutors;

public class MsSqlExecutor : BaseExecutor, IDatabaseExecutor
{
    public MsSqlExecutor(DatabaseConfigOptions? options) : base(options)
    {
    }

    public async Task<string?> ValidateScriptAsync(string script, string dataBaseName)
    {
        SqlConnection? connection = null;

        try
        {
            var connectionString = ConnectionStringBuilder.GetConnectionStringCSharp(Options!.ConnectionStringParams, DataBaseEnum.MySql, dataBaseName);
            connection = new SqlConnection(connectionString);
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
                SqlConnection.ClearAllPools();
                await connection.DisposeAsync();
            }
        }
    }

    public async Task<bool> CreateDatabaseAsync(string databaseName)
    {
        SqlConnection? connection = null;
        try
        {
            var connectionString = ConnectionStringBuilder.GetConnectionStringCSharp(Options!.ConnectionStringParams, DataBaseEnum.MsSql);
            
            connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            var checkDbQuery = $"SELECT database_id FROM sys.databases WHERE name = '{databaseName}'";
            bool dbExists;
            
            await using (var checkCommand = new SqlCommand(checkDbQuery, connection))
            {
                var result = await checkCommand.ExecuteScalarAsync();
                dbExists = result != null;
            }

            if (dbExists)
            {
                var setSingleUserQuery = $"ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                await using (var singleUserCommand = new SqlCommand(setSingleUserQuery, connection))
                {
                    await singleUserCommand.ExecuteNonQueryAsync();
                }

                var dropQuery = $"DROP DATABASE [{databaseName}]";
                await using (var dropCommand = new SqlCommand(dropQuery, connection))
                {
                    await dropCommand.ExecuteNonQueryAsync();
                }
            }

            var createQuery = $"CREATE DATABASE [{databaseName}]";
            await using (var createCommand = new SqlCommand(createQuery, connection))
            {
                await createCommand.ExecuteNonQueryAsync();
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                await connection.CloseAsync();
            }
        }
    }
}