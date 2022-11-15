using DataAccessLogic.Extensions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShoppingLikeFiles.DomainServices.Core;
using ShoppingLikeFiles.DomainServices.Core.Internal;
using ShoppingLikeFiles.DomainServices.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class CaffProcessorServiceCollectionExtensions
{
    public static IServiceCollection AddCaffProcessor(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        //services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<CaffValidatorOptions>, CaffValidatorOptions>());

        services.TryAddSingleton<ICaffValidator, DefaultCaffValidator>();

        services.AddDataAccessLayer();

        return services;
    }

    public static IServiceCollection AddCaffProcessor(this IServiceCollection services, Action<CaffValidatorOptions> setupAction)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (setupAction == null)
        {
            throw new ArgumentNullException(nameof(setupAction));
        }

        services.AddCaffProcessor();
        services.Configure(setupAction);

        return services;
    }
}

