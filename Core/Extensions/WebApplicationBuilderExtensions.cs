namespace Infrastructure.Core.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();

            if(builder.Environment.IsDevelopment())
            {
                builder.Logging.AddDebug();
            }

            builder.Logging.AddConsole();

            return builder;
        }
    }
}