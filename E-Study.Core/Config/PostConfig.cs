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
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Text).IsRequired();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.Votes).HasDefaultValue(0);
            builder.HasOne(x => x.User).WithMany(x => x.Posts).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Course).WithMany(x => x.Posts).HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
