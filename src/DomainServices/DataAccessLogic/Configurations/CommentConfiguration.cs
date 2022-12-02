﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingLikeFiles.DataAccessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DataAccessLogic.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Text)
                //.HasMaxLength(256)
                .IsUnicode(false)
                .IsRequired();


            builder.HasOne(x => x.Caff)
                .WithMany(x => x.Comments)
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
