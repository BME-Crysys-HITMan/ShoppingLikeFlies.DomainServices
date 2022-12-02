namespace ShoppingLikeFiles.DomainServices.DTOs;

public class CaffDTO
{
    public int Id { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public ushort Year { get; set; }
    public byte Month { get; set; }
    public byte Day { get; set; }
    public byte Hour { get; set; }
    public byte Minute { get; set; }
    public string Creator { get; set; } = string.Empty;
    public List<CaffTagDTO> Tags { get; set; } = new();
    public string ThumbnailPath { get; set; } = string.Empty;
    public List<CommentDTO> Comments { get; set; } = new();
    public List<CaptionDTO> Captions { get; set; } = new();
}
