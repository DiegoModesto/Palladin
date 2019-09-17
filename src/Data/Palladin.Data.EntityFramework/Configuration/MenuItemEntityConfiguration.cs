using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Palladin.Data.Entity;

namespace Palladin.Data.EntityFramework.Configuration
{
    public class MenuItemEntityConfiguration : IEntityTypeConfiguration<MenuItemEntity>
    {
        public void Configure(EntityTypeBuilder<MenuItemEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Order).HasColumnType("smallint").IsRequired();
            builder.Property(x => x.Name).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.Path).HasColumnType("varchar(50)").IsRequired();
        }
    }
}
