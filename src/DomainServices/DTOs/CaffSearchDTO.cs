namespace ShoppingLikeFiles.DomainServices.DTOs;

public class CaffSearchDTO
{
    public List<string> Creator { get; set; } = new();
    public List<string> Caption { get; set; } = new();
    public List<string> Tags { get; set; } = new();
}
