using System.Diagnostics;

namespace GameStore.Api.Dtos.Middleware;

public class RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
{

    private readonly RequestDelegate next = next;
    private readonly ILogger<RequestTimingMiddleware> logger = logger;
    
    public async Task InvokeAsync(HttpContext ctx)
    {
        var stopWatch = new Stopwatch();

        try
        {
            stopWatch.Start();
            await next(ctx);
        }
        finally
        {
            stopWatch.Stop();

            var elapsedMilliseconds = stopWatch.ElapsedMilliseconds;
            logger.LogInformation(
                "{RequestMethod} {RequestPath} request took {ElapseMilliseconds}ms",
                ctx.Request.Method,
                ctx.Request.Path,
                elapsedMilliseconds
            );
        }
    }
}