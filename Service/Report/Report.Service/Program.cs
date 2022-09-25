using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Report.Service.Context;
using Report.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql"))
);
builder.Services.AddSingleton(sp => 
    new ConnectionFactory() 
    { 
        Uri = new Uri(builder.Configuration.GetConnectionString("RabbitMQ")) 
    }
);
builder.Services.AddSingleton<RabbitMQClientService>();
builder.Services.AddScoped<IReportService, ReportService>();
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
