namespace ShoppingLikeFiles.DomainServices.Model;

public sealed class CaffCredit
{
    public CaffCredit(ushort year, byte month, byte day, byte hour, byte minute, string creator)
    {
        CreationDate = new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Utc);
        Creator = creator;
        Tags = new List<string>();
    }

    public DateTime CreationDate { get; init; }
    public string Creator { get; init; }
    public IList<string> Tags { get; set; }
}
