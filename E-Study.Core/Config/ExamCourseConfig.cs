using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Study.Core.Models;

namespace E_Study.Core.Config
{
    public class ExamCourseConfig : IEntityTypeConfiguration<ExamCourse>
    {
        public void Configure(EntityTypeBuilder<ExamCourse> builder)
        {
            builder.ToTable("ExamCourses");
            builder.HasKey(x => new { x.ExamId, x.CourseId });

            builder.HasOne(x => x.Course)
                .WithMany(x => x.ExamCourses)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Exam)
                .WithMany(x => x.ExamCourses)
                .HasForeignKey(x => x.ExamId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
