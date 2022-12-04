using Microsoft.EntityFrameworkCore;
using ShoppingLikeFiles.DataAccessLogic.Entities;

namespace ShoppingLikeFiles.DataAccessLogic.Context;

internal class ShoppingLikeFliesDbContext : DbContext
{
    public DbSet<Caff>? Caff { get; set; }
    public DbSet<Comment>? Comment { get; set; }
    public ShoppingLikeFliesDbContext(DbContextOptions<ShoppingLikeFliesDbContext> options) : base(options) { }
}
