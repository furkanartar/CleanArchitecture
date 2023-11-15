using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration // her katman kendi bağımlılığını yönetmesi için service registration'ları ayırıyoruz.
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("nArchitecture")); // development ortamında in memory çalışıyoruz.

        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RentACar"))); // sql server bağlantısını sağlıyoruz. connection string WebApi/appsettings.json'dan geliyor

        services.AddScoped<IBrandRepository, BrandRepository>(); // biri senden IBrandRepository'i isterse ona BrandRepository'i ver diyoruz.

        return services;
    }
}
