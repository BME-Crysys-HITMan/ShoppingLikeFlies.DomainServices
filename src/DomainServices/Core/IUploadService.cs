namespace ShoppingLikeFiles.DomainServices.Core;

/// <summary>
/// This interface represents a class that is able to upload a .caff file to a persistent storage solution.
/// </summary>
internal interface IUploadService
{
    Task<string> UploadFileAsync(byte[] fileContent);
    string UploadFile(byte[] fileContent);
    Task<bool> RemoveFileAsync(string fileLocation);
}
