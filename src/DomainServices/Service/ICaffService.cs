using ShoppingLikeFiles.DomainServices.Model;
namespace ShoppingLikeFiles.DomainServices.Service;

public interface ICaffService
{
    /// <summary>
    /// A tester method the must return "pong"
    /// </summary>
    /// <returns> a simple "pong" string </returns>
    string Ping();

    /// <summary>
    /// Validates a given file.
    /// </summary>
    /// <param name="fileName">Name of the given file</param>
    /// <returns>An optional <see cref="CaffCredit"/> containing all information about the file. <br/> Returns null if file is invalid.</returns>
    CaffCredit? ValidateFile(string fileName);

    /// <summary>
    /// Generates a thumbnail image from a given CAFF file.
    /// </summary>
    /// <param name="fileName">The name of the file. Must be .caff conform file.</param>
    /// <returns>The filename of the generated image.</returns>
    string GetThumbnail(string fileName);
}