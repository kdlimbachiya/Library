using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryCoreApp.Filters
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/Home"))
            {
                var UserId = context.Session.GetInt32("_UserId");
                var IsAdmin = context.Session.GetString("_IsAdmin");

                if (UserId == null || IsAdmin == null)
                {
                    context.Response.Redirect("/Home/Login");
                }
                else if (IsAdmin.ToLower() == "false" && context.Request.Path.ToString().ToLower().Contains("admin"))
                {
                    context.Response.Redirect("/Home/Login");
                }
            }            
            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SessionMiddlewareExtensions
    {
        public static IApplicationBuilder UseSessionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SessionMiddleware>();
        }
    }
}
