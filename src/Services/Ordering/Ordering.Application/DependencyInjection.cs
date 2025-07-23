using BuildingBlocks.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add application services here

            // Infrasucturecter'dan önce bunu oluşturmalıyız. Domain event kısmında kullanılan yapı Mediatr'a bağımlı. Oluşturmazsan hata alır. 

            services.AddMediatR(opt =>
            {
                opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                opt.AddOpenBehavior(typeof(ValidationBehavior<,>));
                opt.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            return services;
        }
    }
}
