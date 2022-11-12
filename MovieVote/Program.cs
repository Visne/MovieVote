using MovieVote.Configuration;
using MovieVote.Db;
using MovieVote.Middleware;
using Newtonsoft.Json;

namespace MovieVote;

public static class Program
{
    private const string ConfigPath = "config.json5";
    public static readonly Config Config;

    static Program()
    {
        using StreamReader file = File.OpenText(ConfigPath);
        
        Config = new JsonSerializer().Deserialize<Config>(new JsonTextReader(file)) ?? throw new Exception($"Missing {ConfigPath}");
    }
    
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            WebRootPath = "Static",
            Args = args,
        });

        builder.WebHost.UseUrls("http://*:" + Config.Port);
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
        builder.Services.AddDbContext<DatabaseContext>();
        builder.Services.AddSingleton(Config);

        var app = builder.Build();
        
        app.UseStaticFiles();
        app.MapControllers();
        app.UseWebSockets(new WebSocketOptions { KeepAliveInterval = TimeSpan.FromMinutes(1) });

        app.UseLogin();

        app.Run();
    }
}