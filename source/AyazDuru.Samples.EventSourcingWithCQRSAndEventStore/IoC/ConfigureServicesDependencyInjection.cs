
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Core.Interfaces;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Infrastructure.Data;
using AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.Services;
using Microsoft.Extensions.DependencyInjection;


namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.IoC
{
    public static class ConfigureServicesDependencyInjection
    {
        public static IServiceCollection AddWeb(this IServiceCollection services)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();            
            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
            return services;
        }
    }
}
