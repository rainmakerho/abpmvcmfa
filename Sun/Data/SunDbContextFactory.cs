using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sun.Data;

public class SunDbContextFactory : IDesignTimeDbContextFactory<SunDbContext>
{
    public SunDbContext CreateDbContext(string[] args)
    {
        SunEfCoreEntityExtensionMappings.Configure();
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<SunDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new SunDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}