using ShoppingLikeFiles.DomainServices.Core;
using ShoppingLikeFiles.DomainServices.Model;

namespace ShoppingLikeFiles.DomainServices.Service;

public class CaffService : ICaffService
{
    private readonly ILogger _logger;
    private readonly ICaffValidator _validator;
    private readonly IThumbnailGenerator _generator;
    private readonly IUploadService _upload;
    private readonly IDataService _data;

    public CaffService(ICaffValidator validator, IThumbnailGenerator generator, IUploadService upload, IDataService data, ILogger logger)
    {
        _logger = logger;
        _validator = validator;
        _generator = generator;
        _upload = upload;
        _data = data;
    }

    public int? UploadFile(string caffFilePath)
    {
        if (!File.Exists(caffFilePath))
        {
            throw new ArgumentNullException(nameof(caffFilePath));
        }

        var result = _validator.ValidateFile(caffFilePath);

        if (result == null)
        {
            _logger.Information("File validation failed!");
            return null;
        }

        var thumbnail = _generator.GenerateThumbnail(caffFilePath);
        if (thumbnail == null)
        {
            _logger.Information("Thumbnail generation failed!");
            return null;
        }
        string filename = $"{result.Creator.ToLower().Replace(' ', '_')}_{DateTime.UtcNow.Ticks}.caff";
        var bytes = File.ReadAllBytes(caffFilePath);
        var path = _upload.UploadFile(bytes, filename);
        if (path == null)
        {
            _logger.Information("Upload failed");
            return null;
        }

        throw new NotImplementedException("Highly impractical");

        _data.CreateCaffAsync(new()
        {
            Caption = "Very nice house",
            Creator = result.Creator,
            Day = (byte)result.CreationDate.Day,
            FilePath = path,
            Hour = (byte)result.CreationDate.Hour,
            Minute = (byte)result.CreationDate.Minute,
            Month = (byte)result.CreationDate.Month,
            Tags = result.Tags.ToList(),
            ThumbnailPath = thumbnail,
            Year = (ushort)result.CreationDate.Year,
        });
    }

    public async Task<int?> UploadFileAsync(string caffFilePath)
    {
        if (!File.Exists(caffFilePath))
        {
            throw new ArgumentNullException(nameof(caffFilePath));
        }

        var result = await _validator.ValidateFileAsync(caffFilePath);

        if (result == null)
        {
            _logger.Information("File validation failed!");
            return null;
        }

        var thumbnail = await _generator.GenerateThumbnailAsync(caffFilePath);
        if (thumbnail == null)
        {
            _logger.Information("Thumbnail generation failed!");
            return null;
        }
        string filename = $"{result.Creator.ToLower().Replace(' ', '_')}_{DateTime.UtcNow.Ticks}.caff";
        var bytes = File.ReadAllBytes(caffFilePath);
        var path = _upload.UploadFile(bytes, filename);
        if (path == null)
        {
            _logger.Information("Upload failed");
            return null;
        }

        int id = await _data.CreateCaffAsync(new()
        {
            Caption = "Very nice house", //TODO get caption from native component.
            Creator = result.Creator,
            Day = (byte)result.CreationDate.Day,
            FilePath = path,
            Hour = (byte)result.CreationDate.Hour,
            Minute = (byte)result.CreationDate.Minute,
            Month = (byte)result.CreationDate.Month,
            Tags = result.Tags.ToList(),
            ThumbnailPath = thumbnail,
            Year = (ushort)result.CreationDate.Year,
        });

        _logger.Debug("Caff entry created");

        return id;
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
