namespace ShoppingLikeFiles.DomainServices.Contract.Incoming;

public class CreateCaffContractDTO
{
    public string FilePath { get; set; } = string.Empty;
    public ushort Year { get; set; }
    public byte Month { get; set; }
    public byte Day { get; set; }
    public byte Hour { get; set; }
    public byte Minute { get; set; }
    public string Creator { get; set; } = string.Empty;
    public List<CaffTagDTO> Tags { get; set; } = new();
    public List<Caption> Captions { get; set; } = new();
    public string ThumbnailPath { get; set; } = string.Empty;
}
