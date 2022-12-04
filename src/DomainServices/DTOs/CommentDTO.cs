namespace ShoppingLikeFiles.DomainServices.DTOs;

public class CommentDTO
{
    public Guid UserId { get; set; }
    public string Text { get; set; } = string.Empty;
}
