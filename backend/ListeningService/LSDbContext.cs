using Microsoft.EntityFrameworkCore;

namespace ZSCourse.ListeningService;

public class LSDbContext : DbContext
{
    public DbSet<Index> Index { get; private set; }
    public DbSet<Language> Language { get; private set; }
    public LSDbContext(DbContextOptions<LSDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
