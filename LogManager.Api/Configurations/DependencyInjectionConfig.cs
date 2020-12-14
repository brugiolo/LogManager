using LogManager.Business.Interfaces;
using LogManager.Business.Services;
using LogManager.Data.Context;
using LogManager.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace LogManager.Api.Configurations
{
    public static class InjecaoDependenciaConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<LogManagerContext>();

            services.AddScoped<IRequestLogRepository, RequestLogRepository>();

            services.AddScoped<IRequestLogService, RequestLogService>();

            services.AddSwaggerGen();

            return services;
        }
    }
}
