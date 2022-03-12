using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
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
        app.MapControllers();

        Database.InitializeDb();

        app.Run();
    }
}