using Volo.Abp.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Sun.Data;

public class SunDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public SunDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        
        /* We intentionally resolving the SunDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<SunDbContext>()
            .Database
            .MigrateAsync();

    }
}
