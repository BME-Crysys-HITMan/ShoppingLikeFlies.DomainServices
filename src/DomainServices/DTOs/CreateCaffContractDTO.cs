namespace ShoppingLikeFiles.DomainServices.Contract.Incoming;

/// <summary>
/// 
/// </summary>
public class CreateCaffContractDTO
{
    /// <summary>
    /// CAption of the image.
    /// </summary>
    public string Caption { get; set; } = string.Empty;

    /// <summary>
    /// Creator of the image.
    /// </summary>
    public string Creator { get; set; } = string.Empty;

    /// <summary>
    /// Day it was crated on.
    /// </summary>
    public byte Day { get; set; }

    /// <summary>
    /// saved file url.
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Hour the image was created on.
    /// </summary>
    public byte Hour { get; set; }

    /// <summary>
    /// Minute the image was created on.
    /// </summary>
    public byte Minute { get; set; }

    /// <summary>
    /// Month it was created on.
    /// </summary>
    public byte Month { get; set; }

    /// <summary>
    /// Tags of the image.
    /// </summary>
    public List<string> Tags { get; set; } = new();

    /// <summary>
    /// Url of the generated thumbnail.
    /// </summary>
    public string ThumbnailPath { get; set; } = string.Empty;

    /// <summary>
    /// Year the image was created on.
    /// </summary>
    public ushort Year { get; set; }
}
