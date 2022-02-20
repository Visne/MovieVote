var config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("config.json")
             .Build();

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseConfiguration(config);
builder.Services.AddRazorPages();

#if DEBUG
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
#endif

var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();

app.Run();