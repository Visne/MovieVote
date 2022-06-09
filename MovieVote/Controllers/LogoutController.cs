using Microsoft.AspNetCore.Mvc;

namespace MovieVote.Controllers;

public class LogoutController : Controller
{
    [Route("logout")]
    public IActionResult LogOut()
    {
        HttpContext.Response.Cookies.Delete("session");
        
        return Redirect("/");
    }
}