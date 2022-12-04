namespace ShoppingLikeFiles.DataAccessLogic.Entities;

/// <summary>
/// Comment entity used for storing comments.
/// </summary>
public class Comment : EntityBase
{
    /// <summary>
    /// Content of the comment.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Foreign key for <see cref="Caff"/> entity.
    /// </summary>
    public int CaffId { get; set; }

    /// <summary>
    /// Navigation property for <see cref="Caff"/> entity.
    /// </summary>
    public Caff? Caff { get; set; }

    /// <summary>
    /// User who made the comment.
    /// </summary>
    public Guid UserId { get; set; }
}
