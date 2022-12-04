using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingLikeFiles.DataAccessLogic.Context;
using ShoppingLikeFiles.DataAccessLogic.Entities;
using ShoppingLikeFiles.DataAccessLogic.Repository;

namespace ShoppingLikeFiles.DataAccessLogic.Extensions;

public static class DataAccessLogicExtensions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connStr = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ShoppingLikeFliesDbContext>(x =>
            x.UseSqlServer(connStr));
        services.AddScoped<ICaffRepository, CaffRepository>();
        services.AddScoped<IGenericRepository<Comment>, GenericRepository<Comment>>();
        return services;
    }
}
