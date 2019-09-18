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

            builder.Property(x => x.Name).HasColumnType("varchar(50)").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Archive).HasColumnType("text").HasMaxLength(2500).IsRequired();

            builder.Property(x => x.CreatedDate).HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();

            /*
            modelBuilder.Entity<MediaPVEntity>().HasKey();
            modelBuilder.Entity<MediaPVEntity>()
                .HasOne<MediaEntity>(a => a.Media)
                .WithMany(b => b.MediaPVEntities)
                .HasForeignKey(c => c.MediaId);
            modelBuilder.Entity<MediaPVEntity>()
                .HasOne<ProjectVulnerabilityEntity>(a => a.ProjectVulnerability)
                .WithMany(b => b.MediaPVEntities)
                .HasForeignKey(c => c.ProjectVulnerabilityId);
             
             
             */
        }
    }
}
