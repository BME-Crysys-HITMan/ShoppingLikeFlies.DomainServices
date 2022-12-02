using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingLikeFiles.DataAccessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DataAccessLogic.Configurations
{
    public class CaffToTagConfiguration : IEntityTypeConfiguration<CaffToTag>
    {
        public void Configure(EntityTypeBuilder<CaffToTag> builder)
        {
            builder.HasKey(x => new { x.CaffId, x.CaffTagId });

            builder.HasOne(x => x.Caff)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.CaffId)
                .HasConstraintName($"fk{nameof(CaffToTag)}To{nameof(Caff)}")
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(x => x.CaffTag)
                .WithMany(x => x.CaffToTags)
                .HasForeignKey(x => x.CaffTagId)
                .HasConstraintName($"fk{nameof(CaffToTag)}To{nameof(CaffTag)}")
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
