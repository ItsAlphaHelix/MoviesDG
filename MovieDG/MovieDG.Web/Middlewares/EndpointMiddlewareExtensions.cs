namespace MovieDG.Web.Middlewares
{
    using MovieDG.Web.Hubs;
    public static class EndpointMiddlewareExtensions
    {
        public static IEndpointRouteBuilder UseCustomEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<ChatHub>("/chatHub");

            endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            return endpoints;
        }
    }
}
