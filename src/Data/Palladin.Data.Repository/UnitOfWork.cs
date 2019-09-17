using Palladin.Data.EntityFramework;
using Palladin.Data.Repository.Interfaces;
using Palladin.Data.Repository.PublicRepository;

namespace Palladin.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly PalladinContext _ctx;

        //public UnitOfWork()
        //{
        //    this._ctx = new PalladinContext();

        //    #region Building repositories
        //    this._menuItemR = new MenuItemRepository(this._ctx);
        //    this._menuR = new MenuRepository(this._ctx);
        //    this._userMenuR = new UserMenuRepository(this._ctx);
        //    this._userR = new UserRepository(this._ctx);
        //    this._vulnerabilityLangR = new VulnerabilityLangRepository(this._ctx);
        //    this._vulnerabilityR = new VulnerabilityRepository(this._ctx);
        //    this._projectR = new ProjectRepository(this._ctx);
        //    #endregion
        //}

        public UnitOfWork(string connectionString)
        {
            if (this._ctx == null)
                this._ctx = new PalladinContext(connectionString);

            #region Building repositories
            this._menuItemR = new MenuItemRepository(this._ctx);
            this._menuR = new MenuRepository(this._ctx);
            this._userMenuR = new UserMenuRepository(this._ctx);
            this._userR = new UserRepository(this._ctx);
            this._vulnerabilityLangR = new VulnerabilityLangRepository(this._ctx);
            this._vulnerabilityR = new VulnerabilityRepository(this._ctx);
            this._projectR = new ProjectRepository(this._ctx);
            this._projectVultR = new ProjectVulnerabilityRepository(this._ctx);
            this._methodProtocolR = new MethodProtocolRepository(this._ctx);
            this._mediaR = new MediaRepository(this._ctx);
            this._mediaPvR = new MediaPVRepository(this._ctx);

            this._tokenR = new TokenRepository(this._ctx);
            #endregion
        }

        public UnitOfWork(PalladinContext ctx)
        {
            this._ctx = ctx;

            #region Building repositories
            this._menuItemR = new MenuItemRepository();
            this._menuR = new MenuRepository();
            this._userMenuR = new UserMenuRepository();
            this._userR = new UserRepository();
            this._vulnerabilityLangR = new VulnerabilityLangRepository();
            this._vulnerabilityR = new VulnerabilityRepository();
            this._projectR = new ProjectRepository();
            this._projectVultR = new ProjectVulnerabilityRepository();
            this._methodProtocolR = new MethodProtocolRepository();
            this._mediaR = new MediaRepository();
            this._mediaPvR = new MediaPVRepository();

            this._tokenR = new TokenRepository();
            #endregion
        }

        public IMenuItemRepository _menuItemR { get; private set; }

        public IMenuRepository _menuR { get; private set; }

        public IUserMenuRepository _userMenuR { get; private set; }

        public IUserRepository _userR { get; private set; }

        public IVulnerabilityLangRepository _vulnerabilityLangR { get; private set; }

        public IVulnerabilityRepository _vulnerabilityR { get; private set; }

        public IProjectRepository _projectR { get; private set; }

        public IProjectVulnerabilityRepository _projectVultR { get; private set; }

        public ITokenRepository _tokenR { get; private set; }

        public IMethodProtocolRepository _methodProtocolR { get; private set; }
        public IMediaRepository _mediaR { get; private set; }
        public IMediaPVRepository _mediaPvR { get; private set; }

        public int Complete()
        {
            return this._ctx.SaveChanges();
        }

        public void Dispose()
        {
            this._ctx.Dispose();
        }
    }
}
