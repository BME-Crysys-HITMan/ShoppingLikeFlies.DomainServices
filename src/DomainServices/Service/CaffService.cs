using Serilog;
using ShoppingLikeFiles.DomainServices.Core;
using ShoppingLikeFiles.DomainServices.Model;

namespace ShoppingLikeFiles.DomainServices.Service;

public class CaffService : ICaffService
{
    private readonly ILogger _logger;
    private readonly ICaffValidator _validator;
    private readonly IThumbnailGenerator _generator;

    public CaffService(ILogger logger, ICaffValidator validator, IThumbnailGenerator generator)
    {
        _logger = logger;
        _validator = validator;
        _generator = generator;
    }

    public string? GetThumbnail(string caffFilePath)
    {
        _logger.Verbose("Called {method} with params: {fileName}", nameof(GetThumbnail), caffFilePath);
        if (string.IsNullOrEmpty(caffFilePath))
        {
            throw new ArgumentNullException(nameof(caffFilePath));
        }

        return _generator.GenerateThumbnail(caffFilePath);
    }

    public Task<string?> GetThumbnailAsync(string caffFilePath)
    {
        _logger.Verbose("Called {method} with params: {fileName}", nameof(GetThumbnailAsync), caffFilePath);
        if (string.IsNullOrEmpty(caffFilePath))
        {
            throw new ArgumentNullException(nameof(caffFilePath));
        }

        return _generator.GenerateThumbnailAsync(caffFilePath);
    }

    public CaffCredit? ValidateFile(string caffFilePath)
    {
        _logger.Verbose("Called {method} with params: {fileName}", nameof(ValidateFile), caffFilePath);
        if (string.IsNullOrEmpty(caffFilePath))
        {
            throw new ArgumentNullException(nameof(caffFilePath));
        }

        return _validator.ValidateFile(caffFilePath);
    }

    public Task<CaffCredit?> ValidateFileAsync(string caffFilePath)
    {
        _logger.Verbose("Called {method} with params: {fileName}", nameof(ValidateFileAsync), caffFilePath);
        if (string.IsNullOrEmpty(caffFilePath))
        {
            throw new ArgumentNullException(nameof(caffFilePath));
        }

        return _validator.ValidateFileAsync(caffFilePath);
    }
}
