namespace ShoppingLikeFiles.DomainServices.Core;

public interface ICaffValidator
{
    bool ValidateFile(string caffFilePath);
    Task<bool> ValidateFileAsync(string caffFilePath);
}

