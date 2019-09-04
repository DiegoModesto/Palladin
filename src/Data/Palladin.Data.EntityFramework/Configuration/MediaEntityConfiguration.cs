using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Palladin.Data.Entity;

namespace Palladin.Data.EntityFramework.Configuration
{
    public class MediaEntityConfiguration : IEntityTypeConfiguration<MediaEntity>
    {
        public void Configure(EntityTypeBuilder<MediaEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Path).HasColumnType("varchar(200)").HasMaxLength(200).IsRequired();

            builder.Property(x => x.CreatedDate).HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}
