using LionBitcoin.Payments.Service.Application;
using LionBitcoin.Payments.Service.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi("docs");

builder.Services
    .AddApplication(builder.Configuration)
    .AddPersistence(builder.Configuration);

var app = builder.Build();

app.UsePersistence();

app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/docs.json", "LionBitcoin.Payments.Service");
});

app.MapControllers();

app.Run();