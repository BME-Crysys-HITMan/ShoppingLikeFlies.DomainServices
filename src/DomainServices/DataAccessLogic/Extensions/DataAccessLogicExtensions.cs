using DataAccessLogic.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLogic.Extensions
{
    public static class DataAccessLogicExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShoppingLikeFliesDbContext>(x =>
                x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IShoppingLikeFliesDbContext, ShoppingLikeFliesDbContext>();
            return services;
        }
    }
}
