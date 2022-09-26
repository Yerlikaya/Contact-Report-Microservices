using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Report.Service.DBContext;
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
builder.Services.AddSingleton<RabbitMQPublisherService>();
builder.Services.AddSingleton<HttpClientService>();

builder.Services.AddHostedService<ExcelReportBackgroundService>();

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
