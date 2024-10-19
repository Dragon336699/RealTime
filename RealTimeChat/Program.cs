using DataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.AddServicesCollection;

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

var webSocketServer = new WebSocketServer();
webSocketServer.ConfigureServices(builder.Services);

var app = builder.Build();


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
