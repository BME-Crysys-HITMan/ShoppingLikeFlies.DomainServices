using ShoppingLikeFiles.DomainServices.Model;

namespace ShoppingLikeFiles.DomainServices.Core;

public interface ICaffValidator
{
    CaffCredit? ValidateFile(string caffFilePath);
    Task<CaffCredit?> ValidateFileAsync(string caffFilePath);
}

