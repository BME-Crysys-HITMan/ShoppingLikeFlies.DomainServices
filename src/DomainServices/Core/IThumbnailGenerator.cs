namespace ShoppingLikeFiles.DomainServices.Core
{
    internal interface IThumbnailGenerator
    {
        string? GenerateThumbnail(string caffFile);
        Task<string?> GenerateThumbnailAsync(string caffFile);
    }
}
