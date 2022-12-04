namespace ShoppingLikeFiles.DataAccessLogic.Entities;

/// <summary>
/// A common base class for any entity stored in the DB.
/// </summary>
public class EntityBase
{
    /// <summary>
    /// Id of an entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The date that the entity is created at.
    /// </summary>
    public DateTime Created { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The date, the entity was last updates.
    /// </summary>
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}
