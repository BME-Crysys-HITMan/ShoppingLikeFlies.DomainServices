using DataAccessLogic.Configurations;
using DataAccessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Context
{
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
            modelBuilder.ApplyConfiguration(new CommentConfigration());
            modelBuilder.ApplyConfiguration(new CaptionConfigration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
