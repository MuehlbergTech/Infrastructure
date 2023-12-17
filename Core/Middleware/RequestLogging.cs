namespace Infrastructure.Core.Middleware
{
    public class RequestLogging
    {
        private readonly RequestDelegate Next;
        private readonly ILogger<RequestLogging> Logger;
        public RequestLogging(RequestDelegate next, ILogger<RequestLogging> logger)
        {
            Next = next;
            Logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context);
                Logger.LogInformation($"[{context.Request.Method}:{context.Request.Path}] - {context.Response.StatusCode}");
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, $"[{context.Request.Method}:{context.Request.Path}] - An exception was thrown");
                throw;
            }
        }
    }
}