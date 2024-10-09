using System.Data;
using Microsoft.Extensions.Options;
using TaskService.Persistence.Factories.Options;

namespace TaskService.Persistence.Factories.Abstraction;

public abstract class SqlConnectionFactory
{
    public ConnectionConfiguration Configuration { get; init; }

    protected SqlConnectionFactory(IOptions<ConnectionConfiguration> configuration)
    {
        Configuration = configuration.Value;
    }
    
    public abstract IDbConnection CreateConnectionAsync();
}