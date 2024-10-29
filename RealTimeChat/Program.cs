using DataAccess.DbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
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

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.UseCors(options => options
    .WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

webSocketServer.Configure(app);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapControllers();

app.Run();
