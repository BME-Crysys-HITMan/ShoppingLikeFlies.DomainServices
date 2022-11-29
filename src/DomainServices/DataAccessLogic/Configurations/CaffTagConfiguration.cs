﻿using DataAccessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Configurations
{
    public class CaffTagConfiguration : IEntityTypeConfiguration<CaffTag>
    {
        public void Configure(EntityTypeBuilder<CaffTag> builder)
        {
            builder.Property(x => x.Tag)
                .IsUnicode(false)
                .IsRequired();
        }
    }
}