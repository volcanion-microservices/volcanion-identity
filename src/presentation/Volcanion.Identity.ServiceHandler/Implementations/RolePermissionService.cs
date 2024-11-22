using Microsoft.Extensions.Logging;
using Volcanion.Core.ServiceHandler.Implementations;
using Volcanion.Identity.Infrastructure.Abstractions;
using Volcanion.Identity.Models.Entities;
using Volcanion.Identity.ServiceHandler.Abstractions;

namespace Volcanion.Identity.ServiceHandler.Implementations;

/// <inheritdoc />
internal class RolePermissionService : BaseService<RolePermission, IRolePermissionRepository>, IRolePermissionService
{
    public RolePermissionService(IRolePermissionRepository repository, ILogger<BaseService<RolePermission, IRolePermissionRepository>> logger) : base(repository, logger)
    {
    }
}
