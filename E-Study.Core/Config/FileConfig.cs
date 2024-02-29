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
    public class FileConfig : IEntityTypeConfiguration<PdfFile>
    {
        public void Configure(EntityTypeBuilder<PdfFile> builder)
        {
            builder.ToTable("Files");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FileName).IsRequired();
            builder.Property(x => x.FilePath).IsRequired();
            builder.HasOne(x => x.Course).WithMany(x => x.PdfFiles).HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
