using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Report.Service.DBContext;
using Report.Service.Models;
using Report.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql"))
);
builder.Services.AddSingleton(sp => 
    new ConnectionFactory() 
    { 
        Uri = new Uri(builder.Configuration.GetConnectionString("RabbitMQ")),
        DispatchConsumersAsync = true,
    }
);
builder.Services.AddSingleton<RabbitMQClientService>();
builder.Services.AddSingleton<IRabbitMQPublisherService, RabbitMQPublisherService>();
builder.Services.AddTransient<HttpClientService>();

builder.Services.AddHostedService<ExcelReportBackgroundService>();

builder.Services.Configure<RiseTechServices>(builder.Configuration.GetSection("RiseTechServices"));

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.UseAuthorization();

app.MapControllers();

app.Run();
