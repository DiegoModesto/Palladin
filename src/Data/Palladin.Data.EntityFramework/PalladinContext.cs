using Microsoft.EntityFrameworkCore;
using Palladin.Data.Entity;
using Palladin.Data.EntityFramework.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Palladin.Data.EntityFramework
{
    public class PalladinContext : DbContext
    {
        #region Entities
        public DbSet<MediaEntity> Medias { get; set; }
        public DbSet<MediaPVEntity> MediaProjectVultis { get; set; }
        public DbSet<MethodProtocolEntity> MethodProtocols { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<ProjectVulnerabilityEntity> ProjectsVulnerability { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<VulnerabilityEntity> Vulnerabilities { get; set; }
        public DbSet<VulnerabilityLangEntity> VulnerabilityLangs { get; set; }
        //Configurações de MENU
        public DbSet<MenuEntity> Menus { get; set; }
        public DbSet<MenuItemEntity> MenusItem { get; set; }
        public DbSet<UserMenuEntity> UserMenus { get; set; }

        //Tabela que gerencia os tokens
        public DbSet<TokenEntity> Tokens { get; set; }
        #endregion

        #region Override's Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region [Configuration]
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.Login).IsUnique();

            modelBuilder.ApplyConfiguration(new MediaEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MethodProtocolEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectVulnerabilityEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VulnerabilityEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VulnerabilityLangEntityConfiguration());

            modelBuilder.ApplyConfiguration(new TokenEntityConfiguration());
            #endregion

            #region [Relationship]
            //ProjectEntity
            modelBuilder.Entity<ProjectEntity>().Ignore(x => x.User);
            modelBuilder.Entity<ProjectEntity>().Ignore(x => x.Customer);
            //modelBuilder.Entity<ProjectEntity>()
            //    .HasOne<UserEntity>(project => project.User)
            //    .WithOne(user => user.Project)
            //    .HasForeignKey<ProjectEntity>(x => x.UserId);

            //modelBuilder.Entity<ProjectEntity>()
            //    .HasOne<UserEntity>(project => project.Customer)
            //    .WithOne(user => user.Project)
            //    .HasForeignKey<ProjectEntity>(x => x.CustomerId);

            //ProjectVulnerabilityEntity
            modelBuilder.Entity<ProjectVulnerabilityEntity>()
                .HasOne<MethodProtocolEntity>(pv => pv.MethodProtocol)
                .WithOne(mp => mp.ProjectVulnerability)
                .HasForeignKey<ProjectVulnerabilityEntity>(x => x.MethodProtocolId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProjectVulnerabilityEntity>()
                .HasOne<ProjectEntity>(pv => pv.Project)
                .WithOne(mp => mp.ProjectVulnerability)
                .HasForeignKey<ProjectVulnerabilityEntity>(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProjectVulnerabilityEntity>()
                .HasOne<VulnerabilityEntity>(pv => pv.Vulnerability)
                .WithOne(mp => mp.ProjectVulnerability)
                .HasForeignKey<ProjectVulnerabilityEntity>(x => x.VulnerabilityId)
                .OnDelete(DeleteBehavior.Cascade);

            //VulnerabilityLangEntity
            modelBuilder.Entity<VulnerabilityLangEntity>()
                .HasOne(vult => vult.Vulnerability)
                .WithMany(lang => lang.Vulnerabilities)
                .HasForeignKey(x => x.VulnerabilityId);

            //MediaPVEntity
            modelBuilder.Entity<MediaPVEntity>().HasKey(x => new { x.MediaId, x.ProjectVulnerabilityId });
            modelBuilder.Entity<MediaPVEntity>()
                .HasOne<MediaEntity>(a => a.Media)
                .WithMany(b => b.MediaPVEntities)
                .HasForeignKey(c => c.MediaId);
            modelBuilder.Entity<MediaPVEntity>()
                .HasOne<ProjectVulnerabilityEntity>(a => a.ProjectVulnerability)
                .WithMany(b => b.MediaPVEntities)
                .HasForeignKey(c => c.ProjectVulnerabilityId);

            //MenuEntity
            modelBuilder.Entity<UserMenuEntity>().HasKey(x => new { x.MenuId, x.UserId });
            //modelBuilder.Entity<UserMenuEntity>()
            //    .HasOne<UserEntity>(a => a.User)

            #endregion

            #region [Seed data]
            //Users
            var clientUser = Guid.NewGuid();
            var esecUser = Guid.NewGuid();
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = clientUser,
                    Login = "cliente",
                    Password = @"5ivWjl+ZjGohSxB1pb/U+w==",
                    CreatedDate = DateTime.Now.AddDays(-90),
                    UserType = Enums.UserType.Client,
                    IsDeleted = false
                },
                new UserEntity
                {
                    Id = esecUser,
                    Login = "esec",
                    Password = @"TKrBGmPmSUdIcUBSa1x0+g==",
                    CreatedDate = DateTime.Now.AddDays(-93),
                    UserType = Enums.UserType.eSecurity,
                    IsDeleted = false
                }
            );

            //Menu
            var menuId1 = Guid.NewGuid();
            var menuId2 = Guid.NewGuid();
            modelBuilder.Entity<MenuEntity>().HasData(
                new MenuEntity
                {
                    Id = menuId1,
                    Name = Enum.GetName(typeof(Enums.UserType), Enums.UserType.Client)
                },
                new MenuEntity
                {
                    Id = menuId2,
                    Name = Enum.GetName(typeof(Enums.UserType), Enums.UserType.eSecurity)
                }
            );

            modelBuilder.Entity<MenuItemEntity>().HasData(
                new MenuItemEntity
                {
                    Id = Guid.NewGuid(),
                    MenuId = menuId1,
                    Name = "Projetos",
                    Path = "/project",
                    Order = 0
                },
                new MenuItemEntity
                {
                    Id = Guid.NewGuid(),
                    MenuId = menuId2,
                    Name = "Projetos",
                    Path = "/admin/project",
                    Order = 0
                },
                new MenuItemEntity
                {
                    Id = Guid.NewGuid(),
                    MenuId = menuId1,
                    Name = "Comparar Projetos",
                    Path = "/compare",
                    Order = 1
                },
                new MenuItemEntity
                {
                    Id = Guid.NewGuid(),
                    MenuId = menuId2,
                    Name = "Vulnerabilidades",
                    Path = "/admin/vulnerability",
                    Order = 1
                },
                new MenuItemEntity
                {
                    Id = Guid.NewGuid(),
                    MenuId = menuId2,
                    Name = "Vinculação",
                    Path = "/admin/join-project",
                    Order = 2
                }
            );

            var userMenuId = Guid.NewGuid();
            modelBuilder.Entity<UserMenuEntity>().HasData(
                new UserMenuEntity
                {
                    MenuId = menuId1,
                    UserId = clientUser
                },
                new UserMenuEntity
                {
                    MenuId = menuId2,
                    UserId = esecUser
                }
            );

            //Vulnerability
            var vulnerabilityId = Guid.NewGuid();
            modelBuilder.Entity<VulnerabilityEntity>().HasData(
                new VulnerabilityEntity
                {
                    Id = vulnerabilityId,
                    CreatedDate = DateTime.Now,
                    CVSS = "AV:N/AV:H/PR:N/UI:N/S:U/C:H/I:H/A:H",
                    IsDeleted = false,
                    Name = "Weak Password Policy",
                    ProjectType = Enums.ProjType.Web,
                    References = "http://www.owasp.org/index.php/Testing_for_Weak_password_policy",
                    RiskFactor = Enums.RiskFactor.Critical,
                    Tags = "password,weak-password,weak-password-policy,password-policy",
                    UserId = esecUser,
                }
            );

            modelBuilder.Entity<VulnerabilityLangEntity>().HasData(
                new VulnerabilityLangEntity
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    LanguageType = Enums.LangType.BR,
                    VulnerabilityId = vulnerabilityId,
                    Description = @"A aplicação não exige que os usuários tenham senhas fortes. A falta de complexidade de senha reduz significamente o espaço de busca ao tentar adivinhar as senhas dos usuários, facilitando ataques de força bruta.
                    Dessa forma,
                    foi possível obter acesso ao sistema utilizando uma conta de usuário que possui senha fraca e de fácil adivinhação.A partir da conta acessada,
                    uma nova conta foi criada.",
                    Remediation = @"Introduza uma política de senha forte (que garanta o tamanho, a complexidade, a reutilização e o envelhecimento da senha) e/ou
                    controles de autenticação adicionais (duplo fator de autenticação)."
                }
            );

            //Method Protocol
            modelBuilder.Entity<MethodProtocolEntity>().HasData(
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "GET"
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "POST"
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "PUT"
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "DELETE"
                }
            );
            #endregion

            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            this.Audit();
            return base.SaveChanges();
        }
        #endregion

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.Audit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Private audit to SaveChanges in DataBase
        /// </summary>
        private void Audit()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedDate = DateTime.UtcNow;
                    ((BaseEntity)entry.Entity).IsDeleted = false;
                }
                if(entry.State == EntityState.Deleted)
                {
                    ((BaseEntity)entry.Entity).IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DBGlobals.DbConnection);
        }
        public PalladinContext()
        {
            base.OnConfiguring(new DbContextOptionsBuilder<PalladinContext>()
                .UseSqlServer(DBGlobals.DbConnection));
        }

        public PalladinContext(string connectionString)
        {
            base.OnConfiguring(new DbContextOptionsBuilder<PalladinContext>()
                .UseSqlServer(connectionString));
        }

        public PalladinContext(DbContextOptions options) : base(options) { }
    }
}
