using E_Study.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Study.Core.Data;

public partial class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext()
    {

    }
    public AppDbContext(DbContextOptions<AppDbContext> ops) : base(ops)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Course).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Exam).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExamCourse).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExamResult).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Grade).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QnAs).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserCourse).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Message).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Comment).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Post).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Event).Assembly);
        
        OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = @"Data Source=.;Database=E_Study;Integrated Security=True";
        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public DbSet<ExamResult> ExamResults { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<ExamCourse> ExamCourses { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<QnAs> QnAs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Event> Events { get; set; }

}
