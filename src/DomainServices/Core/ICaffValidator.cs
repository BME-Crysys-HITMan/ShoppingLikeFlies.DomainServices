namespace ShoppingLikeFiles.DomainServices.Core;

public interface ICaffValidator
{
    bool ValidateFile(string fileName);
    Task<bool> ValidateFileAsync(string fileName);
}

