using Microsoft.EntityFrameworkCore;

namespace Lab6.DbUtils
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddDatabaseOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseType = configuration.GetValue<string>("DatabaseType");
            services.AddDbContext<ProductServicingContext>(options =>
            {
                switch (databaseType)
                {
                    case "MSSQL":
                        options.UseSqlServer(configuration.GetConnectionString("MSSQL"));
                        break;
                    case "Postgres":
                        options.UseNpgsql(configuration.GetConnectionString("Postgres"));
                        break;
                    case "SqlLite":
                        options.UseSqlite(configuration.GetConnectionString("SqlLite"));
                        break;
                    case "InMemory":
                        options.UseInMemoryDatabase("InMemoryDb");
                        break;
                    default:
                        throw new Exception("Invalid Database Type");
                }
            });

            return services;
        }
    }
}
