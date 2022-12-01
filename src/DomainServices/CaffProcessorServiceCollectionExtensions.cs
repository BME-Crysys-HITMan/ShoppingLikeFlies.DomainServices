using DataAccessLogic.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using ShoppingLikeFiles.DomainServices.Core;
using ShoppingLikeFiles.DomainServices.Core.Internal;
using ShoppingLikeFiles.DomainServices.Mappings;
using ShoppingLikeFiles.DomainServices.Options;
using ShoppingLikeFiles.DomainServices.Service;

namespace Microsoft.Extensions.DependencyInjection;

public static class CaffProcessorServiceCollectionExtensions
{
    public static IServiceCollection AddCaffProcessor(this IServiceCollection services, IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.TryAddEnumerable(
            ServiceDescriptor.Transient<IConfigureOptions<CaffValidatorOptions>, CaffValidatorOptionsSetup>());

        services.TryAddEnumerable(
            ServiceDescriptor.Transient<IConfigureOptions<UploadServiceOptions>, UploadServiceOptionsSetup>());

        services.TryAddSingleton<ICaffValidator, DefaultCaffValidator>();
        services.TryAddSingleton<IThumbnailGenerator, DefaultThumbnailGenerator>();
        services.TryAddScoped<ICaffService, CaffService>();
        services.TryAddTransient<IUploadService, UploadService>();

        services.AddDataAccessLayer(configuration);
        services.AddSingleton(_ => MapperConfig.ConfigureAutoMapper());

        return services;
    }

    public static IServiceCollection AddCaffProcessor(this IServiceCollection services, Action<CaffValidatorOptions> setupAction, Action<UploadServiceOptions> uploadActions, IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (setupAction == null)
        {
            throw new ArgumentNullException(nameof(setupAction));
        }

        if (uploadActions == null)
        {
            throw new ArgumentNullException(nameof(uploadActions));
        }

        services.AddCaffProcessor(configuration);
        services.Configure(setupAction);
        services.Configure(uploadActions);

        return services;
    }
}

