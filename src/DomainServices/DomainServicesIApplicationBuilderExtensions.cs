using Microsoft.AspNetCore.Builder;
using ShoppingLikeFiles.DataAccessLogic.Extensions;

namespace ShoppingLikeFiles.DomainServices;

public static class DomainServicesIApplicationBuilderExtensions
{
    public static IApplicationBuilder UseDomainServices(this IApplicationBuilder app, bool isDevelopment)
    {
        return app.UseBusinessDb(isDevelopment);
    }
}
