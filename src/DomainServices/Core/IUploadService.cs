namespace ShoppingLikeFiles.DomainServices.Core;

/// <summary>
/// This interface represents a class that is able to upload a .caff file to a persistent storage solution.
/// </summary>
internal interface IUploadService
{
    /// <summary>
    /// Saves a file to a persistent storage solution.
    /// </summary>
    /// <returns></returns>
    Task<string> UploadFileAsync(string filename, byte[] filecontent);
    Task<bool> RemoveFileAsync(string fileLocation);
}
