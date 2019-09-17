using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Palladin.Data.Entity;

namespace Palladin.Data.EntityFramework.Configuration
{
    public class ProjectEntityConfiguration : IEntityTypeConfiguration<ProjectEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnType("varchar(80)").IsRequired(false);
            builder.Property(x => x.InitialDate).HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.EndDate).HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.ProjectType).HasColumnType("smallint").IsRequired();
            builder.Property(x => x.Subsidiary).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.CustomerId).IsRequired(false);
            builder.Property(x => x.UserId).IsRequired(false);
            
            builder.Property(x => x.CreatedDate).HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}
