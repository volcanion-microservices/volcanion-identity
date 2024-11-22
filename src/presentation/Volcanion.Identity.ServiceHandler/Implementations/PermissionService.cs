using Microsoft.Extensions.Logging;
using Volcanion.Core.ServiceHandler.Implementations;
using Volcanion.Identity.Infrastructure.Abstractions;
using Volcanion.Identity.Models.Entities;
using Volcanion.Identity.ServiceHandler.Abstractions;

namespace Volcanion.Identity.ServiceHandler.Implementations;

/// <inheritdoc />
internal class PermissionService : BaseService<Permission, IPermissionRepository>, IPermissionService
{
    public PermissionService(IPermissionRepository repository, ILogger<BaseService<Permission, IPermissionRepository>> logger) : base(repository, logger)
    {
    }
}
