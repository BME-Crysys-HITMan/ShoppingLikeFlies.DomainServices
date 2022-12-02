namespace ShoppingLikeFiles.DomainServices.DTOs;

public class CaptionDTO
{
    public int Id { get; set; }
    public int CaffId { get; set; }
    public CaffDTO Caff { get; set; } = new();
    public string Text { get; set; } = string.Empty;
}
