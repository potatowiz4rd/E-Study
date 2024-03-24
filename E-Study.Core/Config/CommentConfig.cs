using E_Study.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Core.Config
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Text).IsRequired();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.Votes).HasDefaultValue(0);
            builder.HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
