using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Prometheus;
using ReserveX.Core.Application;
using ReserveX.Core.Application.Interfaces;
using ReserveX.Infraestructure.Persistence;
using System.Text;
using WebApi.Extensions;
using WebApi.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddProblemDetails(configure =>
{
    configure.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
    };
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();
builder.Services.AddAuthenticationExtension(builder.Configuration);
//Layers
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddPersistenceLayer(builder.Configuration);



builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();
app.UseExceptionHandler();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerExtension(app);
}
app.MapHealthChecks("/health");
app.UseMetricServer("/metrics");
app.UseHttpMetrics();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

