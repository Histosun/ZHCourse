using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ZSCourse.ListeningService;

public class IndexConfig : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("T_Language");

        builder.HasKey(x => x.Id);

        builder.HasIndex(it => it.Name)
            .IsUnique();
    }
}
