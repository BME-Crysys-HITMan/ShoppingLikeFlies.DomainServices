namespace ShoppingLikeFiles.DomainServices.Core;

/// <summary>
/// This interface represents a class that is able to upload a .caff file to a persistent storage solution.
/// </summary>
public interface IUploadService
{

    /// <summary>
    /// Saves a file to a persistent storage solution.
    /// </summary>
    /// <returns></returns>
    Task<string> UploadFileAsync(byte[] fileContent, string name);

    Task<bool> RemoveFileAsync(string fileLocation);
    string UploadFile(byte[] fileContent, string name);
}
