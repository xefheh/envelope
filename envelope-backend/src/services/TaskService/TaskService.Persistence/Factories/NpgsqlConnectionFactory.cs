using System.Data;
using Microsoft.Extensions.Options;
using Npgsql;
using TaskService.Persistence.Factories.Abstraction;
using TaskService.Persistence.Factories.Options;

namespace TaskService.Persistence.Factories;

/// <summary>
/// Фабрика подключений PostgreSQL
/// </summary>
public class NpgsqlConnectionFactory : SqlConnectionFactory
{
    private readonly string _connectionString;

    public NpgsqlConnectionFactory(IOptions<ConnectionConfiguration> configuration, string connectionString) : base(configuration)
    {
        _connectionString =
            $"Server={Configuration.Database};" +
            $"Port={Configuration.Port};" +
            $"Database={Configuration.Database};" +
            $"User Id={Configuration.User};" +
            $"Password={Configuration.Password};";
    }
    
    public override IDbConnection CreateConnectionAsync()
    {
        return new NpgsqlConnection(_connectionString);
    }
}