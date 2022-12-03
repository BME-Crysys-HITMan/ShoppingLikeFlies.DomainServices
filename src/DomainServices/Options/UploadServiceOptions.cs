namespace ShoppingLikeFiles.DomainServices.Options;

public class UploadServiceOptions
{
    public string DirectoryPath { get; set; } = string.Empty;
    public bool ShouldUploadToAzure { get; set; } = false;
    public string ConnectionString { get; set; } = string.Empty;
}
