using Palladin.Data.EntityFramework;
using Palladin.Data.Repository.Interfaces;
using Palladin.Data.Repository.PublicRepository;
using System.Threading.Tasks;

namespace Palladin.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly PalladinContext _ctx;

        public UnitOfWork(string connectionString)
            : this(ctx: new PalladinContext(connectionString))
        {
        }
        public UnitOfWork(PalladinContext ctx)
        {
            this._ctx = ctx;
            #region Building repositories
            _mediaPvR = new MediaPVRepository(this._ctx);
            _mediaR = new MediaRepository(this._ctx);
            _menuR = new MenuRepository(this._ctx);
            _methodR = new MethodProtocolRepository(this._ctx);
            _projectR = new ProjectRepository(this._ctx);
            _projectVultR = new ProjectVulnerabilityRepository(this._ctx);
            _refreshTokenR = new RefreshTokenRepository(this._ctx);
            _roleR = new RoleRepository(this._ctx);
            _userMenuR = new UserMenuRepository(this._ctx);
            _userR = new UserRepository(this._ctx);
            _userRoleR = new UserRoleRepository(this._ctx);
            _vultR = new VulnerabilityRepository(this._ctx);
            _vultLangR = new VulnerabilityLangRepository(this._ctx);
            _companyR = new CompanyRepository(this._ctx);
            #endregion
        }

        public IMediaPVRepository _mediaPvR { get; private set; }
        public IMediaRepository _mediaR { get; private set; }
        public IMenuRepository _menuR { get; private set; }
        public IMethodProtocolRepository _methodR { get; private set; }
        public IProjectRepository _projectR { get; private set; }
        public IProjectVulnerabilityRepository _projectVultR { get; private set; }
        public IRefreshTokenRepository _refreshTokenR { get; private set; }
        public IRoleRepository _roleR { get; private set; }
        public IUserMenuRepository _userMenuR { get; private set; }
        public IUserRepository _userR { get; private set; }
        public IUserRoleRepository _userRoleR { get; private set; }
        public IVulnerabilityRepository _vultR { get; private set; }
        public IVulnerabilityLangRepository _vultLangR { get; private set; }
        public ICompanyRepository _companyR { get; set; }

        public int Complete()
        {
            return this._ctx.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await this._ctx.SaveChangesAsync();
        }

        public void Dispose()
        {
            this._ctx.Dispose();
        }
    }
}
