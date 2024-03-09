using System.Diagnostics;

namespace TaskManagerAPI.Middleware;

public class ResponseTimeMiddleware : IMiddleware
{
    private readonly ILogger<ResponseTimeMiddleware> _logger;
    private readonly Stopwatch _stopWatch;

    public ResponseTimeMiddleware(ILogger<ResponseTimeMiddleware> logger)
    {
        _logger = logger;
        _stopWatch = new Stopwatch();
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _stopWatch.Start();
        await next.Invoke(context);
        _stopWatch.Stop();

        var elapsedMilliseconds = _stopWatch.ElapsedMilliseconds;
        if (elapsedMilliseconds / 1000 > 5)
        {
            var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {elapsedMilliseconds} ms";
            _logger.LogInformation(message);
        }
    }
}