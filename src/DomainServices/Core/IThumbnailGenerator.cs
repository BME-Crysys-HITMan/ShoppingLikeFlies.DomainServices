namespace ShoppingLikeFiles.DomainServices.Core
{
    internal interface IThumbnailGenerator
    {
        string? GenerateThumbnail(string caffFilePath);
        Task<string?> GenerateThumbnailAsync(string caffFilePath);
    }
}
