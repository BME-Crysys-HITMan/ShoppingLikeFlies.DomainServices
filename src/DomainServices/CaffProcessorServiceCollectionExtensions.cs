using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using ShoppingLikeFiles.DataAccessLogic.Extensions;
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

        services.Configure<CaffValidatorOptions>(x => x.Validator = "CAFF_processor.exe");

        services.TryAddEnumerable(
            ServiceDescriptor.Transient<IConfigureOptions<UploadServiceOptions>, UploadServiceOptionsSetup>());

        services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<ThumbnailGeneratorOptions>, ThumbnailGeneratorOptionsSetup>());

        services.TryAddSingleton<INativeCommunicator, NativeCommunicator>();
        services.TryAddTransient<ICaffValidator, DefaultCaffValidator>();
        services.TryAddTransient<IThumbnailGenerator, DefaultThumbnailGenerator>();
        services.TryAddScoped<ICaffService, CaffService>();
        services.TryAddTransient<IUploadService, UploadService>();
        services.TryAddTransient<IDataService, DataService>();
        services.TryAddTransient<IPaymentService, PaymentService>();

        services.AddDataAccessLayer(configuration);
        services.AddAutoMapper(typeof(DomainServiesProfile).Assembly);

        return services;
    }

    public static IServiceCollection AddCaffProcessor(
        this IServiceCollection services, 
        Action<CaffValidatorOptions> validatorAction, 
        Action<UploadServiceOptions> uploadActions, 
        Action<ThumbnailGeneratorOptions> thumbnailGeneratorActions,
        IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (validatorAction == null)
        {
            throw new ArgumentNullException(nameof(validatorAction));
        }

        if (uploadActions == null)
        {
            throw new ArgumentNullException(nameof(uploadActions));
        }

        if (thumbnailGeneratorActions is null)
        {
            throw new ArgumentNullException(nameof(thumbnailGeneratorActions));
        }

        services.AddCaffProcessor(configuration);
        services.Configure(validatorAction);
        services.Configure(uploadActions);
        services.Configure(thumbnailGeneratorActions);

        return services;
    }
}

