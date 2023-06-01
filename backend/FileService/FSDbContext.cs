using Microsoft.EntityFrameworkCore;
using ZSCourse.FileService.Entities;

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
