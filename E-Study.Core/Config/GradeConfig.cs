﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using E_Study.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Core.Config
{
    public class GradeConfig : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grades");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Attempt).HasDefaultValue(0);
            builder.Property(x => x.DateTime).HasDefaultValueSql("GETDATE()");
            builder.HasOne(x => x.User).WithMany(x => x.Grades).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Exam).WithMany(x => x.Grades).HasForeignKey(x => x.ExamId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
