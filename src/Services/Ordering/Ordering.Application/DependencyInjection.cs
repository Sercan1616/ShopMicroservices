using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add application services here

            // Infrasucturecter'dan önce bunu oluşturmalıyız. Domain event kısmında kullanılan yapı Mediatr'a bağımlı. Oluşturmazsan hata alır. 

            services.AddMediatR(opt =>
            {
                opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                opt.AddOpenBehavior(typeof(ValidationBehavior<,>));
                opt.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            services.AddFeatureManagement();

            services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
