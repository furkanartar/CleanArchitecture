using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServiceRegistration //Application katmanının IOC süreçleri burada işlenecek. Bu sayede Program.cs temiz kalacak.
{

    //extension yazdığımız için class ve fonksiyon static olmalı
    //neyi extend edeceksek onu parametrede this ile geçiyoruz, burada IServiceCollection'ı extend ediyoruz
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }

}
