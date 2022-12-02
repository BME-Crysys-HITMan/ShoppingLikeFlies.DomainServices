namespace ShoppingLikeFiles.DomainServices.DTOs;

public class CaffSearchDTO
{
    public string Creator { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
}
