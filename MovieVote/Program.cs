using MovieVote.Api.Discord;
using MovieVote.Configuration;
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
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.UseUrls("http://*:" + Config.Port);
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

        var app = builder.Build();
        
        app.UseStaticFiles();
        app.MapControllers();

        app.UseLogin();

        app.Run();
    }
}