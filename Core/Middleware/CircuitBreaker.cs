namespace Infrastructure.Core.Middleware
{
    public class CircuitBreaker
    {
        private readonly RequestDelegate Next;
        private readonly IConfiguration Config;
        public CircuitBreaker(RequestDelegate next, IConfiguration config)
        {
            Next = next;
            Config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var timeout = Config.GetValue<int>("CircuitBreakerTimeoutMS");
            
            using var cancellationTokenSource = new CancellationTokenSource(timeout);
            context.Items.Add("CancellationTokenSource", cancellationTokenSource);

            var request = Task.Run(() => Next(context), cancellationTokenSource.Token);
            var completedTask = await Task.WhenAny(request, Task.Delay(timeout));
            if(completedTask != request)
            {
                cancellationTokenSource.Cancel();
                throw new TimeoutException($"Request timed out after {timeout}ms");
            }
        }
    }
}