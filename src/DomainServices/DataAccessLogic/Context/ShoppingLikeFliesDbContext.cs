using Microsoft.EntityFrameworkCore;
using ShoppingLikeFiles.DataAccessLogic.Entities;

namespace ShoppingLikeFiles.DataAccessLogic.Context;


/// <summary>
/// Entity framework context to access a database
/// </summary>
internal class ShoppingLikeFliesDbContext : DbContext
{
    /// <summary>
    /// <see cref="Entities.Caff" /> table.
    /// </summary>
    public DbSet<Caff>? Caff { get; set; }

    /// <summary>
    /// <see cref="Entities.Comment"/> table.
    /// </summary>
    public DbSet<Comment>? Comment { get; set; }


    public ShoppingLikeFliesDbContext(DbContextOptions<ShoppingLikeFliesDbContext> options) : base(options) { }
}
