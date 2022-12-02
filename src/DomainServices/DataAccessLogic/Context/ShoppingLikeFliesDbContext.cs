using Microsoft.EntityFrameworkCore;
using ShoppingLikeFiles.DataAccessLogic.Configurations;
using ShoppingLikeFiles.DataAccessLogic.Entities;

namespace ShoppingLikeFiles.DataAccessLogic.Context;

internal class ShoppingLikeFliesDbContext : DbContext
{
    public virtual DbSet<Caff> Caff { get; set; }
    public virtual DbSet<CaffTag> CaffTag { get; set; }
    public virtual DbSet<CaffToTag> CaffToTag { get; set; }
    public virtual DbSet<Comment> Comment { get; set; }
    public ShoppingLikeFliesDbContext(DbContextOptions<ShoppingLikeFliesDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
        modelBuilder.ApplyConfiguration(new CaffConfiguration());
        modelBuilder.ApplyConfiguration(new CaffTagConfiguration());
        modelBuilder.ApplyConfiguration(new CaffToTagConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new CaptionConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
