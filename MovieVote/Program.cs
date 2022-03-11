using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;
using MovieVote.Api.Discord;
using MovieVote.Api.Discord.Json;
using MovieVote.Configuration;
using MovieVote.Db;
using Newtonsoft.Json;

namespace MovieVote;

public static class Program
{
    private const string ConfigPath = "config.json";
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
        builder.Services.AddRazorPages();

#if DEBUG
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
#endif

        var app = builder.Build();
        
        app.UseStaticFiles();
        app.MapRazorPages();

        /*app.MapGet("/oauth/discord", async context =>
        {

        });*/
        
        Database.InitializeDb();

        app.Run();
    }
}