using Microsoft.AspNetCore.Builder;

namespace bopg.api.account
{
    public static class APIHandlerExtensions
    {
        public static IApplicationBuilder UseAPIHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Handler.APIHandler>();
        }
    }
}
