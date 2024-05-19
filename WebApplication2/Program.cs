using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using OpenTelemetry.Exporter;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddOpenTelemetry(options =>
{
    options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName));
    options.IncludeScopes = true;
    options.AddOtlpExporter(otlpOptions =>
    {
        otlpOptions.Protocol = OtlpExportProtocol.HttpProtobuf;
        otlpOptions.Endpoint = new Uri("http://192.168.0.201:5341/ingest/otlp/v1/logs");
    });
});

// Add services to the container.

builder.Services.AddControllers();

var otel = builder.Services.AddOpenTelemetry();

// Configure OpenTelemetry Resources with the application name
otel.ConfigureResource(resource => resource
    .AddService(builder.Environment.ApplicationName));


otel.WithTracing(tracing =>
{
    tracing.AddSource("MyTraceSample");
    tracing.AddAspNetCoreInstrumentation();
    tracing.AddOtlpExporter(otlpOptions =>
    {
        otlpOptions.Protocol = OtlpExportProtocol.HttpProtobuf;
        otlpOptions.Endpoint = new Uri("http://192.168.0.201:5341/ingest/otlp/v1/traces");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
