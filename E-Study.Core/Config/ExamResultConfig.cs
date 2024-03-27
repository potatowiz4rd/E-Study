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
    public class ExamResultConfig : IEntityTypeConfiguration<ExamResult>
    {
        public void Configure(EntityTypeBuilder<ExamResult> builder)
        {
            builder.ToTable("ExamResults");            
            builder.HasOne(x => x.Exam).WithMany(x => x.ExamResults).HasForeignKey(x => x.ExamId);
            builder.HasOne(x => x.QnAs).WithMany(x => x.ExamResults).HasForeignKey(x => x.QnAsId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(x => x.User).WithMany(x => x.ExamResults).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
