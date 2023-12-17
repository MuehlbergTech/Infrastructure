using Infrastructure.Extensions;

namespace Infrastructure
{
    public class AppHost
    {
        public DateTimeOffset StartTime;
        public IHostEnvironment Env;
        public IConfiguration Config;

        public WebApplication BuildApplication(WebApplicationBuilder builder)
        {
            var app = builder.Build();

            return app.ConfigureMiddleware()
                .ConfigureDocumentation()
                .ConfigureHttpsRedirection()
                .ConfigureControllers()
                .ConfigureHealthChecks();
        }

        public void ConfigureHostServices(IServiceCollection services)
        {
            services.ConfigureFrameworkServices(Env);
        }

        public WebApplicationBuilder CreateBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Env = builder.Environment;
            Config = builder.Configuration;
            return builder;
        }

        public WebApplication ConfigureApplication(WebApplicationBuilder builder)
        {
            builder.ConfigureLogging();

            return BuildApplication(builder);
        }
    }
}