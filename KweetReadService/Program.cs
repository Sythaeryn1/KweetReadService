using KweetReadService.Consumers;
using KweetReadService.Data;
using KweetReadService.Models.RabbitMq;
using KweetReadService.Services;
using MassTransit;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoDbSettings>(ServiceProvider =>
ServiceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

builder.Services.AddScoped<IMongoRepository, MongoRepository>();
builder.Services.AddScoped<IReadService, ReadService>();


builder.Services.AddControllers();

var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<KweetConsumer>();


    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(rabbitMqSettings.Uri, "vh1", c =>
        {
            c.Username(rabbitMqSettings.UserName);
            c.Password(rabbitMqSettings.Password);
        });
        cfg.ReceiveEndpoint("kweet-message", e =>
        {
            e.ConfigureConsumer<KweetConsumer>(ctx);
        });

        cfg.ConfigureEndpoints(ctx);
       
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
