namespace ShoppingLikeFiles.DomainServices.Model;

public record CaffDomainModel(Guid id, string name, IEnumerable<string> tags);

