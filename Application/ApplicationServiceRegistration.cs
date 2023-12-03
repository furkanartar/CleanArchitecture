using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using FluentValidation;
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
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); //Fleuntvalidation'ı ekledik. Bu core'daki RequestValidationBehavior'a constr'ına validators'ları gönderecek
        services.AddMediatR(configuration => //MediatR'ı ekledik
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            //mediatr'ye bir request çalıştıracaksan aşağıdaki middleware'lardan geçir bakalım diyoruz
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));//validation middleware'ını devreye alıyoruz
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));//transaction middleware'ını devreye alıyoruz
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));//Cache middleware'ını devreye alıyoruz
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));//Cache remover middleware'ını devreye alıyoruz
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
