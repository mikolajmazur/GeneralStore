using GeneralStore.Api.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GeneralStore.Api.Config
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Store Api", 
                    Version = "v1",
                    Description = "Sample description of the api ....."
                });
            });

            return services;
        }

        public static IServiceCollection AddDataAccessService(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
            });

            return services;
        }
    }
}
