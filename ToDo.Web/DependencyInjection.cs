using ToDo.Infrastructure.Data;

namespace ToDo.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddExceptionHandler<CustomExceptionHandler>();

            return services;
        }
    }
}
