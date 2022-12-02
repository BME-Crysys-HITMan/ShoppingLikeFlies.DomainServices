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
    public class CaptionConfiguration : IEntityTypeConfiguration<Caption>
    {
        public void Configure(EntityTypeBuilder<Caption> builder)
        {
            builder.Property(x => x.Text)
                //.HasMaxLength(256)
                .IsUnicode(false)
                .IsRequired();

            builder.HasOne(x => x.Caff)
                .WithMany(x => x.Captions)
                .HasForeignKey(x => x.CaffId)
                .HasConstraintName($"fk{nameof(Caption)}To{nameof(Caff)}")
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
