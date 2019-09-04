using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Palladin.Data.Entity;

namespace Palladin.Data.EntityFramework.Configuration
{
    public class MethodProtocolEntityConfiguration : IEntityTypeConfiguration<MethodProtocolEntity>
    {
        public void Configure(EntityTypeBuilder<MethodProtocolEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.ProjectType).HasColumnType("smallint").IsRequired();

            builder.Property(x => x.CreatedDate).HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}
