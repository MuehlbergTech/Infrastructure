namespace Infrastructure.Core.Extensions
{
    public static class ServiceExtensions
    {

        public static IServiceCollection ConfigureFrameworkServices(this IServiceCollection services, IHostEnvironment env)
        {
            services.AddControllers();
            services.AddHealthChecks();

            if(env.IsDevelopment())
            {
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();
            }
                
            return services;
        }
    }
}