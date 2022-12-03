using Microsoft.Extensions.Options;

namespace ShoppingLikeFiles.DomainServices.Options;

internal sealed class UploadServiceOptionsSetup : IConfigureOptions<UploadServiceOptions>
{
    public void Configure(UploadServiceOptions options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (string.IsNullOrEmpty(options.DirectoryPath))
        {
            options.DirectoryPath = ".\\file_directory";
        }

        if (options.ShouldUploadToAzure && string.IsNullOrEmpty(options.ConnectionString))
        {
            throw new InvalidDataException("ConnectionString must be set if used with azure");
        }
    }
}
