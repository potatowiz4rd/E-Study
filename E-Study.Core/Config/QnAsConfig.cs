using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using E_Study.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Core.Config
{
    public class QnAsConfig : IEntityTypeConfiguration<QnAs>
    {
        public void Configure(EntityTypeBuilder<QnAs> builder)
        {
            builder.ToTable("QnAs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Option1).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Option2).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Option3).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Option4).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Answer).IsRequired();
            builder.HasOne(x => x.Exam).WithMany(x => x.QnAs).HasForeignKey(x => x.ExamId).OnDelete(DeleteBehavior.ClientSetNull);         
        }
    }
}
