using Factory.Publishers;
using MassTransit;

var builder = WebApplication.CreateBuilder();

builder.Services.AddMassTransit(conf =>
{
    conf.AddConsumers(typeof(Program).Assembly);
    conf.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddHostedService<SalaryPublisher>();
builder.Services.AddHostedService<ProductStockPublisher>();


var app = builder.Build();

app.Run();
