using eShopAsp.Infrastructure.Data;
using eShopAsp.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShopAsp.Infrastructure;

public static class Dependencies 
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        bool useOnlyInMemoryDatabase = false;
        if (configuration["UseOnlyInMemoryDatabase"] != null)
            useOnlyInMemoryDatabase = bool.Parse(configuration["UseOnlyInMemoryDatabase"]!);

        if (useOnlyInMemoryDatabase) 
        {
            services.AddDbContext<CatalogContext>(c 
                => c.UseInMemoryDatabase("Catalog"));

            services.AddDbContext<AppIdentityDbContext>(c 
                => c.UseInMemoryDatabase("Identity"));

            // services.AddDbContext<ContributorsContext>(c 
            //     => c.UseInMemoryDatabase("Contributors"));
        }
        else 
        {
            services.AddDbContext<CatalogContext>(c 
                => c.UseSqlite(configuration.GetConnectionString("CatalogConnection")));

            services.AddDbContext<AppIdentityDbContext>(c
                => c.UseSqlite(configuration.GetConnectionString("IdentityConnection")));

            // services.AddDbContext<ContributorsContext>(c 
            //     => c.UseSqlite(configuration.GetConnectionString("ContributorConnection")));
        }
    }
}