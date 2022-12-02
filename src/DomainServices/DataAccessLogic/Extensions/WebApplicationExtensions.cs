using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ShoppingLikeFiles.DataAccessLogic.Context;

namespace ShoppingLikeFiles.DataAccessLogic.Extensions
{
    public static class WebApplicationExtensions
    {
        public static IApplicationBuilder UseBusinessDb(this IApplicationBuilder app, bool isDevelopment)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ShoppingLikeFliesDbContext>();
            var logger = scope.ServiceProvider.GetService<ILogger>();

            if (isDevelopment)
            {
                logger?.Debug("{database} begin to delete", nameof(ShoppingLikeFliesDbContext));
                context.Database.EnsureDeleted();
                logger?.Debug("{database} begin to delete", nameof(ShoppingLikeFliesDbContext));
            }

            logger?.Debug("{database} created", nameof(ShoppingLikeFliesDbContext));
            context.Database.EnsureCreated();
            logger?.Information("{database} created", nameof(ShoppingLikeFliesDbContext));

            return app;
        }
    }
}
