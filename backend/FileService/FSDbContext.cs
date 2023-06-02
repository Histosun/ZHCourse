using Microsoft.EntityFrameworkCore;

namespace ZSCourse.FileService;

public class FSDbContext : DbContext
{
    public DbSet<UploadedFile> UploadedFile { get; private set; }

    public FSDbContext(DbContextOptions<FSDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
