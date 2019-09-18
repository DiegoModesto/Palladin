using System;

namespace Palladin.Data.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMediaPVRepository _mediaPvR { get; }
        IMediaRepository _mediaR { get; }
        IMenuRepository _menuR { get; }
        IMethodProtocolRepository _methodR { get; }
        IProjectRepository _projectR { get; }
        IProjectVulnerabilityRepository _projectVultR { get; }
        IRefreshTokenRepository _refreshTokenR { get; }
        IRoleRepository _roleR { get; }
        IUserMenuRepository _userMenuR { get; }
        IUserRepository _userR { get; }
        IUserRoleRepository _userRoleR { get; }
        IVulnerabilityRepository _vultR { get; }
        IVulnerabilityLangRepository _vultLangR { get; }

        int Complete();
    }
}
