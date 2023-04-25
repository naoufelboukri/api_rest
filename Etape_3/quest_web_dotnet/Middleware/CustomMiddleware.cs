namespace quest_web.Middleware
{
    public class CustomMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        async Task IMiddleware.InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await _next(context);

            if (context.Response.StatusCode == 401 && context.Response.Headers.ContainsKey("WWW-Authenticate"))
            {
                await context.Response.WriteAsJsonAsync("Vous n'êtes pas autorisé à accéder à cette ressource.");
            }
        }
    }
}
