using DataAccessLogic.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShoppingLikeFiles.DomainServices.Core;
using ShoppingLikeFiles.DomainServices.Core.Internal;
using ShoppingLikeFiles.DomainServices.Mappings;
using ShoppingLikeFiles.DomainServices.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class CaffProcessorServiceCollectionExtensions
{
    public static IServiceCollection AddCaffProcessor(this IServiceCollection services, IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        //services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<CaffValidatorOptions>, CaffValidatorOptions>());

        services.TryAddSingleton<ICaffValidator, DefaultCaffValidator>();

        services.AddDataAccessLayer(configuration);
        services.AddSingleton(_ => MapperConfig.ConfigureAutoMapper());

        return services;
    }

    public static IServiceCollection AddCaffProcessor(this IServiceCollection services, Action<CaffValidatorOptions> setupAction, IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (setupAction == null)
        {
            throw new ArgumentNullException(nameof(setupAction));
        }

        services.AddCaffProcessor(configuration);
        services.Configure(setupAction);

        return services;
    }
}

