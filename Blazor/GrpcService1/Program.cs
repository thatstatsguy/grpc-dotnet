using GrpcService1.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
app.MapGrpcService<WeatherService>()
    .RequireCors("AllowAll")
    .EnableGrpcWeb();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
