using Core.Application.Rules;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServiceRegistration //Application katmanının IOC süreçleri burada işlenecek. Bu sayede Program.cs temiz kalacak.
{

    //extension yazdığımız için class ve fonksiyon static olmalı
    //neyi extend edeceksek onu parametrede this ile geçiyoruz, burada IServiceCollection'ı extend ediyoruz
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly()); // AutoMapper'i ekledik
        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules)); // BaseBusinessRules sınıfından türetilen (inherit edilen) tüm sınıfları scope'a eklemeyi sağlıyor, bu sayede her business sınıfını car, brand, model vs tek tek eklemeyeceğiz.
        services.AddMediatR(configuration => //MediatR'ı ekledik
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }

    public static IServiceCollection AddSubClassesOfType(this IServiceCollection services, Assembly assembly, Type type, Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null)
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();

        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);

        return services;
    }
}
