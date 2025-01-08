# SwaggerUI.MiniProfilerIntegration

`SwaggerUI.MiniProfilerIntegration` is a library that integrates the [MiniProfiler](https://miniprofiler.com/) UI with Swagger UI in ASP.NET Core applications. This package provides extension methods for Swagger UI configuration to display MiniProfiler badges directly in the Swagger UI page, allowing developers to easily visualize performance data.

## Features

- Adds MiniProfiler badge to Swagger UI.
- Customizable position and behavior of the profiler UI.
- Provides integration options such as `showTrivial`, `maxTracesToShow`, and more.
- Includes a sample ASP.NET Core project demonstrating the integration.

## Installation

To install the `SwaggerUI.MiniProfilerIntegration` package, use the NuGet package manager or the .NET CLI:

### Using NuGet Package Manager

```bash
Install-Package SwaggerUI.MiniProfilerIntegration
```

### Using .NET CLI

```bash
dotnet add package SwaggerUI.MiniProfilerIntegration
```

## Usage

### 1. Set up MiniProfiler in your ASP.NET Core project

Ensure that you have the [MiniProfiler NuGet package](https://www.nuget.org/packages/MiniProfiler/) installed and configured in your project.

Add the following code to configure MiniProfiler in your `Startup.cs` or `Program.cs`:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMiniProfiler(options =>
    {
        // Optional: Customize MiniProfiler settings
        options.RouteBasePath = "/profiler";
        options.PopupMaxTracesToShow = 5;
    }).AddEntityFramework(); // Add additional integrations as needed
}
```

### 2. Integrate MiniProfiler with Swagger UI

In your `Startup.cs` or `Program.cs`, use the extension methods provided by the library to render MiniProfiler in Swagger UI.

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // Add MiniProfiler to the request pipeline
    app.UseMiniProfiler();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Add MiniProfiler integration to Swagger UI
        options.RenderMiniProfiler(app);
    });

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```

You can customize the MiniProfiler settings like this:

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Customize MiniProfiler display
        options.RenderMiniProfiler(
            app,
            position: RenderPosition.Left,
            showTrivial: true,
            maxTracesToShow: 10
        );
    });
}
```

Alternatively, you can pass in custom rendering options:

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RenderMiniProfiler(app, new RenderOptions
        {
            Position = RenderPosition.Left,
            ShowTrivial = true,
            MaxTracesToShow = 10
        });
    });
}
```

### 3. Sample Project

A sample ASP.NET Core project demonstrating the integration is available in the `AspNetCore.Sample` folder. This project includes:

- Swagger UI with MiniProfiler integration.
- A simple API to visualize the performance profiling.

To run the sample project:

1. Open the `AspNetCore.Sample` project in Visual Studio or your preferred editor.
2. Build and run the project.
3. Navigate to `/swagger` to view the Swagger UI with the MiniProfiler badge.

## Contributing

If you'd like to contribute to this project, feel free to fork the repository, create a pull request, and submit issues.

1. Fork the repository
2. Create a new branch for your feature or bugfix
3. Commit your changes
4. Open a pull request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.