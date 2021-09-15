using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLS.Domain;

namespace PLS.Infrastructure.Persistence.Configurations
{
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.HasIndex(t => t.Floor)
                .IsUnique();
        }
    }
}
