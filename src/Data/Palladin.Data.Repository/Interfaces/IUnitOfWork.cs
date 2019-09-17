using System;

namespace Palladin.Data.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMenuItemRepository _menuItemR { get; }
        IMenuRepository _menuR { get; }
        IUserMenuRepository _userMenuR { get; }
        IUserRepository _userR { get; }
        IVulnerabilityLangRepository _vulnerabilityLangR { get; }
        IVulnerabilityRepository _vulnerabilityR { get; }
        IProjectRepository _projectR { get; }
        IProjectVulnerabilityRepository _projectVultR { get; }
        ITokenRepository _tokenR { get; }
        IMethodProtocolRepository _methodProtocolR { get; }
        IMediaRepository _mediaR { get; }
        IMediaPVRepository _mediaPvR { get; }

        int Complete();
    }
}
