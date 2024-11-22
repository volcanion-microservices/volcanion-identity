using Microsoft.Extensions.Logging;
using Volcanion.Core.Infrastructure.Implementations;
using Volcanion.Identity.Infrastructure.Abstractions;
using Volcanion.Identity.Models.Context;
using Volcanion.Identity.Models.Entities;

namespace Volcanion.Identity.Infrastructure.Implementations;

internal class RolePermissionRepository : BaseRepository<RolePermission, ApplicationDbContext>, IRolePermissionRepository
{
    public RolePermissionRepository(ApplicationDbContext context, ILogger<BaseRepository<RolePermission, ApplicationDbContext>> logger) : base(context, logger)
    {
    }
}
