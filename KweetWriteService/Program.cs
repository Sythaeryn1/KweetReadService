using KweetWriteService.Data;
using KweetWriteService.Services;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using KweetWriteService.Models.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//voor gebruik
//builder.Services.AddDbContext<KweetDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("KweetsDefaultConnection")));
//voor migrations
builder.Services.AddDbContext<KweetDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("KweetsMigrationsConnection")));

builder.Services.AddScoped<IKweetService, KweetWriteService.Services.KweetService>();

var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
builder.Services.AddMassTransit(mt => mt.AddMassTransit(x =>
{
    x.UsingRabbitMq((cntxt, cfg) =>
    {
        cfg.Host(rabbitMqSettings.Uri, "vh1", c =>
        {
            c.Username(rabbitMqSettings.UserName);
            c.Password(rabbitMqSettings.Password);
        });
    });
}));

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

