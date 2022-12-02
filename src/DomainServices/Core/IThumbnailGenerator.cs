namespace ShoppingLikeFiles.DomainServices.Core
{
    public interface IThumbnailGenerator
    {
        string? GenerateThumbnail(string caffFilePath);
        Task<string?> GenerateThumbnailAsync(string caffFilePath);
    }
}
