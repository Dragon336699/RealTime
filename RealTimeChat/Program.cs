using DataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.AddServicesCollection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<RealTimeDapperContext>();

builder.Services.ConfigureTransient();

builder.Services.AddDbContext<RealTimeDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("MyDb"),
        b => b.MigrationsAssembly("DataAccess")
    )
);

builder.Services.AddAutoMapper(typeof(Program));

var webSocketServer = new WebSocketServer();
webSocketServer.ConfigureServices(builder.Services);

builder.Services.ConfigureServices();

var app = builder.Build();

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);



webSocketServer.Configure(app);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
