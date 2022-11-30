namespace ShoppingLikeFiles.DomainServices.DTOs;

public class CommentDTO
{
    public int Id { get; set; }
    public int CaffId { get; set; }
    public CaffDTO Caff { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; }
}
