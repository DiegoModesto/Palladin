using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Palladin.Data.Entity;

namespace Palladin.Data.EntityFramework.Configuration
{
    public class TokenEntityConfiguration : IEntityTypeConfiguration<TokenEntity>
    {
        public void Configure(EntityTypeBuilder<TokenEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.ExpirationDate).HasColumnType("datetime2").IsRequired();

            builder.Property(x => x.CreatedDate).HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}
