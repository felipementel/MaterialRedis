using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.API.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue("x-correlation-id", out StringValues correlationIds);

            var correlationId = correlationIds.FirstOrDefault() ?? Guid.NewGuid().ToString();

            httpContext.Response.Headers.Add("x-correlation-id", correlationId.ToString());

            //using (LogContext.PushProperty("CorrelationId", correlationId.ToString()))
            //{
            //    context
            //        .Response
            //        .OnStarting(
            //            state =>
            //            {
            //                var httpContext = (HttpContext)state;
            //                httpContext.Response.Headers.Add("x-correlation-id", correlationId.ToString());

            //                var keysName = string.Join(",", httpContext.Response.Headers.Keys);
            //                httpContext.Response.Headers.Add("Access-Control-Expose-Headers", keysName);
            //                return Task.CompletedTask;
            //            },
            //            httpContext
            //        );

            //    await _next(httpContext);
            //}

            await _next(httpContext);
        }
    }
}