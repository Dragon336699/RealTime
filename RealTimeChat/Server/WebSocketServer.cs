using RealTimeChat.Server;

public class WebSocketServer
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR();
    }
    public void Configure(IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<ChatHub>("/chathub");
        });
    }
}
