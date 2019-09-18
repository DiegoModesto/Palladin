using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Palladin.Data.Entity;

namespace Palladin.Data.EntityFramework.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Login).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.Password).HasColumnType("text").IsRequired();
            builder.Property(x => x.UserType).HasColumnType("smallint").IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar(100)");
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValueSql("0");

            builder.Property(x => x.CreatedDate).HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}
