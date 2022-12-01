namespace ShoppingLikeFiles.DomainServices.Contract.Incoming;

public class CreateCaffContractDTO
{
    public string FilePath { get; set; }
    public ushort Year { get; set; }
    public byte Month { get; set; }
    public byte Day { get; set; }
    public byte Hour { get; set; }
    public byte Minute { get; set; }
    public string Creator { get; set; }
    public List<CaffTagDTO> Tags { get; set; }
    public List<Caption> Captions { get; set; }
    public string ThumbnailPath { get; set; }
}
