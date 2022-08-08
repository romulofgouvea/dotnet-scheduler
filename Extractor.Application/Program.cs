using Extractor.Domain.Models;
using Extractor.Services.Configs;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.Configure<AppSettingsConfiguration>(configuration.GetSection("Params"));
builder.Services.DapperConfiguration();
builder.Services.SwaggerConfiguration();
builder.Services.CronJobConfiguration(configuration);
BuilderConfigureMiddleWare(builder.Services);


var app = builder.Build();
AppConfigureMiddleWare(app, app.Environment);
app.UseSwaggerSetup();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://localhost:{port}");


#region BUILDER
void BuilderConfigureMiddleWare(IServiceCollection services)
{
    services.AddResponseCompression();
    services.AddAuthentication();
    services.AddAuthorization();
    services.AddCors();
    services.AddRouting();
    services.AddEndpointsApiExplorer();
}

void RoutesConfiguration(WebApplication app)
{
    app.MapGet("/", () => "Application started! " + app.Environment.EnvironmentName);
}
#endregion

#region APP
void AppConfigureMiddleWare(WebApplication app, IWebHostEnvironment env)
{
    if (builder.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("");
    }

    app.UseResponseCompression();

    app.UseRouting();
    app.UseCors("default");
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/",
            async context => { await context.Response.WriteAsync("Application started! " + env.EnvironmentName); });
    });
}
#endregion