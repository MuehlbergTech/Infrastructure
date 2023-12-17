using HealthChecks.UI.Client;
using Infrastructure.Middleware;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Infrastructure.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            app.UseMiddleware<RequestLogging>();
            app.UseMiddleware<CircuitBreaker>();
            
            return app;
        }

        public static WebApplication ConfigureDocumentation(this WebApplication app)
        {
            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }
            
            return app;
        }

        public static WebApplication ConfigureControllers(this WebApplication app)
        {
            app.MapControllers();
            
            return app;
        }

        public static WebApplication ConfigureHttpsRedirection(this WebApplication app)
        {
            if(!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }
            
            return app;
        }

        public static WebApplication ConfigureHealthChecks(this WebApplication app)
        {
            app.MapHealthChecks("/service/status", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            
            return app;
        }
    }
}