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
        public DbSet<MenuEntity> Menus { get; set; }
        public DbSet<MethodProtocolEntity> MethodProtocols { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<ProjectVulnerabilityEntity> ProjectsVulnerability { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserMenuEntity> UsersMenu { get; set; }
        public DbSet<UserRoleEntity> UsersRoles { get; set; }
        public DbSet<VulnerabilityEntity> Vulnerabilities { get; set; }
        public DbSet<VulnerabilityLangEntity> VulnerabilityLangs { get; set; }
        //Configurações de MENU

        public DbSet<UserMenuEntity> UserMenus { get; set; }

        //Tabela que gerencia os tokens
        public DbSet<RefreshTokenEntity> Tokens { get; set; }
        #endregion

        #region Override's Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region [Configuration]
            modelBuilder.ApplyConfiguration(new MediaEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MenuEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MethodProtocolEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectVulnerabilityEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VulnerabilityEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VulnerabilityLangEntityConfiguration());

            modelBuilder
                .Entity<MediaPVEntity>()
                .HasKey(x => new { x.MediaId, x.ProjectVulnerabilityId })
                .HasName("FK_Media_PV");
            modelBuilder
                .Entity<UserMenuEntity>()
                .HasKey(x => new { x.MenuId, x.UserId })
                .HasName("FK_Menu_User");
            #endregion

            #region [Seed data]
            //Users
            var clientUser = Guid.NewGuid();
            var esecUser = Guid.NewGuid();
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = clientUser,
                    Login = DateTime.UtcNow.ToString("yyyyMMddHHmm"),
                    Password = @"5ivWjl+ZjGohSxB1pb/U+w==",
                    UserName = "Diego Sanches",
                    Email = "diego@cliente.com",
                    CreatedDate = DateTime.Now.AddDays(-90),
                    UserType = Enums.UserType.Client,
                    IsDeleted = false
                },
                new UserEntity
                {
                    Id = esecUser,
                    Login = DateTime.UtcNow.ToString("yyyyMMddHHmm"),
                    Password = @"5ivWjl+ZjGohSxB1pb/U+w==",
                    UserName = "Administrador [eSecurity]",
                    Email = "adm@esecurity.com",
                    CreatedDate = DateTime.Now.AddDays(-90),
                    UserType = Enums.UserType.eSecurity,
                    IsDeleted = false
                }
            );

            //Menu
            var menuId1 = Guid.NewGuid();
            var menuId2 = Guid.NewGuid();
            var menuId3 = Guid.NewGuid();
            modelBuilder.Entity<MenuEntity>().HasData(
                new MenuEntity
                {
                    Id = menuId1,
                    Name = Enum.GetName(typeof(Enums.UserType), Enums.UserType.Client),
                    Order = 0,
                    Path = "/projects"
                },
                new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Enum.GetName(typeof(Enums.UserType), Enums.UserType.Client),
                    Order = 0,
                    Path = "/projects/view-details",
                    ParentId = menuId1
                },

                new MenuEntity
                {
                    Id = menuId2,
                    Name = Enum.GetName(typeof(Enums.UserType), Enums.UserType.eSecurity),
                    Order = 0,
                    Path = "/projects"
                },
                new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Enum.GetName(typeof(Enums.UserType), Enums.UserType.eSecurity),
                    Order = 0,
                    Path = "/projects/create",
                    ParentId = menuId2
                },
                new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Enum.GetName(typeof(Enums.UserType), Enums.UserType.eSecurity),
                    Order = 1,
                    Path = "/projects/join",
                    ParentId = menuId2
                },

                new MenuEntity
                {
                    Id = menuId3,
                    Name = Enum.GetName(typeof(Enums.UserType), Enums.UserType.eSecurity),
                    Order = 1,
                    Path = "/vulnerabilities"
                },
                new MenuEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Enum.GetName(typeof(Enums.UserType), Enums.UserType.eSecurity),
                    Order = 0,
                    Path = "/vulnerabilities/create",
                    ParentId = menuId3
                }
            );

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
                },
                new UserMenuEntity
                {
                    MenuId = menuId3,
                    UserId = esecUser
                }
            );

            //Vulnerability
            var vulnerabilityId01 = Guid.NewGuid();
            var vulnerabilityId02 = Guid.NewGuid();
            modelBuilder.Entity<VulnerabilityEntity>().HasData(
                new VulnerabilityEntity
                {
                    Id = vulnerabilityId01,
                    CreatedDate = DateTime.Now,
                    CVSS = "AV:N/AV:H/PR:N/UI:N/S:U/C:H/I:H/A:H",
                    IsDeleted = false,
                    Name = "Weak Password Policy",
                    ProjectType = Enums.ProjType.Web,
                    References = @"http://www.owasp.org/index.php/Testing_for_Weak_password_policy",
                    RiskFactor = Enums.RiskFactor.Critical,
                    Tags = "password,weak-password,weak-password-policy,password-policy",
                    UserId = esecUser,
                },
                new VulnerabilityEntity
                {
                    Id = vulnerabilityId02,
                    CreatedDate = DateTime.Now,
                    CVSS = "AV:N/AV:H/PR:N/UI:N/S:U/C:H/I:H/A:H",
                    IsDeleted = false,
                    Name = "Buffer overflow",
                    ProjectType = Enums.ProjType.Web,
                    References = @"http://www.owasp.org/index.php/buffer_overflow_field",
                    RiskFactor = Enums.RiskFactor.Critical,
                    Tags = "field-validation,field-weak",
                    UserId = esecUser,
                }
            );

            modelBuilder.Entity<VulnerabilityLangEntity>().HasData(
                new VulnerabilityLangEntity
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    LanguageType = Enums.LangType.US,
                    VulnerabilityId = vulnerabilityId01,
                    Description = @"The application does not require users to have strong passwords. The lack of password complexity significantly reduces search space by trying to guess user passwords, facilitating brute force attacks.
                    Thus,
                    it was possible to gain access to the system using a user account that has weak password and easy guessing. From the accessed account,
                    A new account has been created.",
                    Remediation = @"Enter a strong password policy (which ensures password length, complexity, reuse and aging) and / or
                    additional authentication controls (double factor authentication)."
                },
                new VulnerabilityLangEntity
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    LanguageType = Enums.LangType.BR,
                    VulnerabilityId = vulnerabilityId01,
                    Description = @"A aplicação não exige que os usuários tenham senhas fortes. A falta de complexidade de senha reduz significamente o espaço de busca ao tentar adivinhar as senhas dos usuários, facilitando ataques de força bruta.
                    Dessa forma,
                    foi possível obter acesso ao sistema utilizando uma conta de usuário que possui senha fraca e de fácil adivinhação.A partir da conta acessada,
                    uma nova conta foi criada.",
                    Remediation = @"Introduza uma política de senha forte (que garanta o tamanho, a complexidade, a reutilização e o envelhecimento da senha) e/ou
                    controles de autenticação adicionais (duplo fator de autenticação)."
                },
                new VulnerabilityLangEntity
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    LanguageType = Enums.LangType.ES,
                    VulnerabilityId = vulnerabilityId01,
                    Description = @"La aplicación no requiere que los usuarios tengan contraseñas seguras. La falta de complejidad de la contraseña reduce significativamente el espacio de búsqueda al tratar de adivinar las contraseñas de los usuarios, lo que facilita los ataques de fuerza bruta.
                    De esa forma,
                    fue posible obtener acceso al sistema utilizando una cuenta de usuario que tiene una contraseña débil y fácil de adivinar.
                    Se ha creado una nueva cuenta.",
                    Remediation = @"Ingrese una política de contraseña segura (que asegure la longitud, complejidad, reutilización y antigüedad de la contraseña) y / o
                    controles de autenticación adicionales (autenticación de doble factor)."
                }
            );

            //Method Protocol
            var methodPost = Guid.NewGuid();
            modelBuilder.Entity<MethodProtocolEntity>().HasData(
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "GET",
                    ProjectType = Enums.ProjType.Web
                },
                new MethodProtocolEntity
                {
                    Id = methodPost,
                    IsDeleted = false,
                    Name = "POST",
                    ProjectType = Enums.ProjType.Web
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "PUT",
                    ProjectType = Enums.ProjType.Web
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "DELETE",
                    ProjectType = Enums.ProjType.Web
                },

                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "IP",
                    ProjectType = Enums.ProjType.Rede
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "FTP",
                    ProjectType = Enums.ProjType.Rede
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "SSH",
                    ProjectType = Enums.ProjType.Rede
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "SSL",
                    ProjectType = Enums.ProjType.Rede
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "TELNET",
                    ProjectType = Enums.ProjType.Rede
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "SMTP",
                    ProjectType = Enums.ProjType.Rede
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "POP3",
                    ProjectType = Enums.ProjType.Rede
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "IMAP4",
                    ProjectType = Enums.ProjType.Rede
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "HTTP",
                    ProjectType = Enums.ProjType.Rede
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "HTTPS",
                    ProjectType = Enums.ProjType.Rede
                },
                new MethodProtocolEntity
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Name = "SIP",
                    ProjectType = Enums.ProjType.Rede
                }
            );

            //Project
            var projectId = Guid.NewGuid();
            modelBuilder.Entity<ProjectEntity>().HasData(
                new ProjectEntity
                {
                    Id = projectId,
                    CustomerId = clientUser,
                    UserId = esecUser,
                    InitialDate = DateTime.UtcNow.AddDays(60),
                    EndDate = DateTime.UtcNow.AddDays(67),
                    Name = @"Projeto 01 [Web]: <br />Domínio: http://www.siteinseguro.com.br/",
                    ProjectType = Enums.ProjType.Web,
                    Subsidiary = "Subsidiária"
                }
            );

            modelBuilder.Entity<ProjectVulnerabilityEntity>().HasData(
                new ProjectVulnerabilityEntity
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow.AddDays(50),
                    Environment = "register.php",
                    Port = "80",
                    FiledOrCookieName = "password",
                    ProjectId = projectId,
                    UserId = esecUser,
                    MethodProtocolId = methodPost,
                    VulnerabilityId = vulnerabilityId01,
                    Status = Enums.ProjStatus.New
                },
                new ProjectVulnerabilityEntity
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow.AddDays(50),
                    Environment = "register.php",
                    Port = "80",
                    FiledOrCookieName = "retype-password",
                    ProjectId = projectId,
                    UserId = esecUser,
                    MethodProtocolId = methodPost,
                    VulnerabilityId = vulnerabilityId01,
                    Status = Enums.ProjStatus.New
                },
                new ProjectVulnerabilityEntity
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow.AddDays(50),
                    Environment = "contato.php",
                    Port = "80",
                    FiledOrCookieName = "name",
                    ProjectId = projectId,
                    UserId = esecUser,
                    MethodProtocolId = methodPost,
                    VulnerabilityId = vulnerabilityId02,
                    Status = Enums.ProjStatus.New
                }
            );

            var roleAdm = Guid.NewGuid();
            var roleContributor = Guid.NewGuid();
            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity
                {
                    Id = roleAdm,
                    Level = 9,
                    Name = "admin",
                },
                new RoleEntity
                {
                    Id = roleContributor,
                    Level = 0,
                    Name = "contributor"
                }
            );

            modelBuilder.Entity<UserRoleEntity>().HasData(
                new UserRoleEntity
                {
                    RoleId = roleAdm,
                    UserId = esecUser
                },
                new UserRoleEntity
                {
                    RoleId = roleContributor,
                    UserId = esecUser
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
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.Audit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        #endregion

        #region Private Method's
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
                if (entry.State == EntityState.Deleted)
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
        #endregion

        public PalladinContext() : this(DBGlobals.DbConnection) { }
        public PalladinContext(string connectionString)
        {
            connectionString = connectionString ?? DBGlobals.DbConnection;
            base.OnConfiguring(new DbContextOptionsBuilder<PalladinContext>()
                .UseSqlServer(connectionString));
        }
    }
}
