using Microsoft.Extensions.Options;

namespace ShoppingLikeFiles.DomainServices.Options;

internal sealed class CaffValidatorOptionsSetup : IConfigureOptions<CaffValidatorOptions>
{
    public void Configure(CaffValidatorOptions options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (string.IsNullOrEmpty(options.Validator))
        {
            options.Validator = "CAFF_Processor";
        }
    }
}
