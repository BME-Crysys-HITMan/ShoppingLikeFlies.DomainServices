using DataAccessLogic.Context;
using DataAccessLogic.Entities;
using DataAccessLogic.Repository;
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
            services.AddScoped<IGenericRepository<Caff>, GenericRepository<Caff>>();
            services.AddScoped<IGenericRepository<Comment>, GenericRepository<Comment>>();
            services.AddScoped<IGenericRepository<Caption>, GenericRepository<Caption>>();
            services.AddScoped<IGenericRepository<CaffTag>, GenericRepository<CaffTag>>();
            return services;
        }
    }
}
