using DataAccessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Configurations
{
    public class CommentConfigration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Text)
                //.HasMaxLength(256)
                .IsUnicode(false)
                .IsRequired();

            builder.HasKey(x => new { x.CaffId, x.UserId });

            builder.HasOne(x => x.Caff)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.CaffId)
                .HasConstraintName($"fk{nameof(Comment)}To{nameof(Caff)}")
                .OnDelete(DeleteBehavior.ClientCascade);

           /* builder.HasOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName($"fk{nameof(Comment)}To{nameof(User)}")
                .OnDelete(DeleteBehavior.ClientCascade);*/
        }
    }
}
