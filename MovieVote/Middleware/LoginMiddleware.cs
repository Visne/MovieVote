using MovieVote.Db;

namespace MovieVote.Middleware;

public class LoginMiddleware
{
    private readonly RequestDelegate _next;

    public LoginMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Cookies.TryGetValue("session", out string? sessionId) || sessionId == null)
        {
            // User has no session cookie, do not attempt to login
            await _next(context);
            return;
        }

        var data = Database.GetStoredUserData(sessionId);

        if (data == null)
        {
            // User has no valid session, do not attempt to login
            await _next(context);
            return;
        }
        
        
        context.Items.Add(LoginItems.LoggedIn, true);
        context.Items.Add(LoginItems.Name, data.Name);
        context.Items.Add(LoginItems.Avatar, data.Avatar);

        await _next(context);
    }
}

public static class LoginMiddlewareExtensions
{
    public static IApplicationBuilder UseLogin(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoginMiddleware>();
    }
}

public static class LoginItems
{
    public const string LoggedIn = "LoggedIn";
    public const string Name = "Name";
    public const string Avatar = "Avatar";
}