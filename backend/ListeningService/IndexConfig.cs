using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ZSCourse.ListeningService;

public class IndexConfig : IEntityTypeConfiguration<Index>
{
    public void Configure(EntityTypeBuilder<Index> builder)
    {
        builder.ToTable("T_Index");
    }
}
