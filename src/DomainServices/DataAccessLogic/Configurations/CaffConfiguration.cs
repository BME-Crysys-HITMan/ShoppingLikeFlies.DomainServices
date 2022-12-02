using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingLikeFiles.DataAccessLogic.Entities;

namespace ShoppingLikeFiles.DataAccessLogic.Configurations
{
    public class CaffConfiguration : IEntityTypeConfiguration<Caff>
    {
        public void Configure(EntityTypeBuilder<Caff> builder)
        {
            builder.Property(x => x.FilePath)
                //.HasMaxLength(256)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.CreationDateTime)
                .HasColumnType(nameof(DateTime))
                .IsRequired();

            builder.Property(x => x.Creator)
                .IsUnicode(false);

            builder.Property(x => x.ThumbnailPath)
                .IsUnicode(false)
                .IsRequired();
        }
    }
}
