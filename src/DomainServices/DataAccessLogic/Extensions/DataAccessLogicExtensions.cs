using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLogic.Extensions
{
    public static class DataAccessLogicExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddDbContext<DbContext>(x =>
                x.UseSqlServer(""));
            return services;
        }
    }
}
