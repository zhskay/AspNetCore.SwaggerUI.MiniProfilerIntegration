using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Profiling;
using StackExchange.Profiling.Internal;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace Microsoft.AspNetCore.Builder;

public static class SwaggerUIWithMiniProfilerExtensions
{
    /// <summary>
    /// Register the SwaggerUI middleware with provided options.
    /// </summary>
    public static IApplicationBuilder UseSwaggerUIWithMiniProfiler(this IApplicationBuilder app, SwaggerUIOptions options)
    {
        MiniProfilerOptions miniProfilerOptions = app.ApplicationServices.GetRequiredService<IOptions<MiniProfilerOptions>>().Value;

        if (MiniProfiler.StartNew() is MiniProfiler profiler)
        {
            options.HeadContent += Render.Includes(profiler, $"{miniProfilerOptions.RouteBasePath}/", true);
        }

        return app.UseSwaggerUI(options);
    }

    /// <summary>
    /// Register the SwaggerUI middleware with optional setup action for DI-injected options.
    /// </summary>
    public static IApplicationBuilder UseSwaggerUIWithMiniProfiler(
        this IApplicationBuilder app,
        Action<SwaggerUIOptions> setupAction = null)
    {
        MiniProfilerOptions miniProfilerOptions = app.ApplicationServices.GetRequiredService<IOptions<MiniProfilerOptions>>().Value;

        Action<SwaggerUIOptions> overridenSetupAction = (options) =>
        {
            setupAction?.Invoke(options);

            if (MiniProfiler.StartNew() is MiniProfiler profiler)
            {
                options.HeadContent += Render.Includes(profiler, $"{miniProfilerOptions.RouteBasePath}/", true);
            }
        };

        return app.UseSwaggerUI(overridenSetupAction);
    }
}
