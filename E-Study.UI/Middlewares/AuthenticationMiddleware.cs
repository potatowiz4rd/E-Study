using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace E_Study.UI.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated && !context.Request.Path.StartsWithSegments("/Identity"))
            {
                // Redirect to the login page if the user is not authenticated and the request is not already for the login page
                context.Response.Redirect("/login");
                return;
            }

            await _next(context);
        }
    }
}
