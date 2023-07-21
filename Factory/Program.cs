using Factory.Publishers;
using MassTransit;

var builder = WebApplication.CreateBuilder();

//builder.Services.AddHostedService<ProductStockPublisher>();
builder.Services.AddHostedService<SalaryPublisher>();

builder.Services.AddMassTransit(conf =>
{
    conf.SetKebabCaseEndpointNameFormatter();
    conf.UsingRabbitMq((context, cfg) =>
    {
		cfg.Host("localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
		cfg.ConfigureEndpoints(context);
	});
});




var app = builder.Build();

app.Run();
