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

            builder.Ignore(x => x.UserId);
            builder.Ignore(x => x.CustomerId);

            //builder
            //    .HasOne(x => x.User)
            //    .WithOne(x => x.Project)
            //    .HasForeignKey<ProjectEntity>(x => x.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder
            //    .HasIndex(x => x.UserId)
            //    .IsUnique(false);


            //builder
            //    .HasOne(x => x.Customer)
            //    .WithOne(x => x.Project)
            //    .HasForeignKey<ProjectEntity>(x => x.CustomerId)
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.SetNull);

            //builder
            //    .HasIndex(x => x.CustomerId)
            //    .IsUnique(false);
        }
    }
}
