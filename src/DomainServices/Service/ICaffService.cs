using ShoppingLikeFiles.DomainServices.Model;
namespace ShoppingLikeFiles.DomainServices.Service;

public interface ICaffService
{
    /// <summary>
    /// Validates a given file.
    /// </summary>
    /// <param name="caffFilePath">Name of the given file</param>
    /// <returns>An optional <see cref="CaffCredit"/> containing all information about the file. <br/> Returns null if file is invalid.</returns>
    CaffCredit? ValidateFile(string caffFilePath);

    /// <summary>
    /// Generates a thumbnail image from a given CAFF file.
    /// </summary>
    /// <param name="caffFilePath">The name of the file. Must be .caff conform file.</param>
    /// <returns>The filename of the generated image.</returns>
    string? GetThumbnail(string caffFilePath);

    Task<CaffCredit?> ValidateFileAsync(string caffFilePath);

    Task<string?> GetThumbnailAsync(string caffFilePath);
}