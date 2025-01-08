using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Profiling;
using StackExchange.Profiling.Internal;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Provides extension methods for configuring MiniProfiler integration with Swagger UI.
/// </summary>
public static class SwaggerUIOptionsExtensions
{
    /// <summary>
    /// Includes MiniProfiler script to the Swagger UI page head.
    /// </summary>
    /// <param name="options">The Swagger UI options to extend.</param>
    /// <param name="app">The application builder used to access application services.</param>
    /// <param name="position">The UI position to render the profiler in (defaults to <see cref="MiniProfilerBaseOptions.PopupRenderPosition"/>).</param>
    /// <param name="showTrivial">Whether to show trivial timings column initially or not (defaults to <see cref="MiniProfilerBaseOptions.PopupShowTrivial"/>).</param>
    /// <param name="showTimeWithChildren">Whether to show time with children column initially or not (defaults to <see cref="MiniProfilerBaseOptions.PopupShowTimeWithChildren"/>).</param>
    /// <param name="maxTracesToShow">The maximum number of profilers to show (before the oldest is removed - defaults to <see cref="MiniProfilerBaseOptions.PopupMaxTracesToShow"/>).</param>
    /// <param name="showControls">Whether to show the controls (defaults to <see cref="MiniProfilerBaseOptions.ShowControls"/>).</param>
    /// <param name="startHidden">Whether to start hidden (defaults to <see cref="MiniProfilerBaseOptions.PopupStartHidden"/>).</param>
    public static void RenderMiniProfiler(
        this SwaggerUIOptions options,
        IApplicationBuilder app,
        RenderPosition? position = null,
        bool? showTrivial = null,
        bool? showTimeWithChildren = null,
        int? maxTracesToShow = null,
        bool? showControls = null,
        bool? startHidden = null)
    {
        MiniProfilerOptions profilerOptions = app.ApplicationServices
            .GetRequiredService<IOptions<MiniProfilerOptions>>().Value;

        if (profilerOptions.StartProfiler() is MiniProfiler profiler)
        {
            options.HeadContent += Render.Includes(
                profiler,
                path: profilerOptions.RouteBasePath + "/",
                isAuthorized: true,
                position: position,
                showTrivial: showTrivial,
                showTimeWithChildren: showTimeWithChildren,
                maxTracesToShow: maxTracesToShow,
                showControls: showControls,
                startHidden: startHidden);

            profiler.Stop();
        }
    }

    /// <summary>
    /// Includes MiniProfiler badge script to the Swagger UI page head using specified rendering options.
    /// </summary>
    /// <param name="options">The Swagger UI options to extend.</param>
    /// <param name="app">The application builder used to access application services.</param>
    /// <param name="renderOptions">The option overrides (if any) to use rendering this MiniProfiler.</param>
    public static void RenderMiniProfiler(
        this SwaggerUIOptions options,
        IApplicationBuilder app,
        RenderOptions? renderOptions)
    {
        MiniProfilerOptions profilerOptions = app.ApplicationServices
            .GetRequiredService<IOptions<MiniProfilerOptions>>().Value;

        if (profilerOptions.StartProfiler() is MiniProfiler profiler)
        {
            options.HeadContent += Render.Includes(
                profiler,
                path: profilerOptions.RouteBasePath + "/",
                isAuthorized: true,
                renderOptions: renderOptions);

            profiler.Stop();
        }
    }
}
