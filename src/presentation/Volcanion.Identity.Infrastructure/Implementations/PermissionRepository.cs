using Microsoft.Extensions.Logging;
using Volcanion.Core.Infrastructure.Implementations;
using Volcanion.Identity.Infrastructure.Abstractions;
using Volcanion.Identity.Models.Context;
using Volcanion.Identity.Models.Entities;

namespace Volcanion.Identity.Infrastructure.Implementations;

internal class PermissionRepository : BaseRepository<Permission, ApplicationDbContext>, IPermissionRepository
{
    public PermissionRepository(ApplicationDbContext context, ILogger<BaseRepository<Permission, ApplicationDbContext>> logger) : base(context, logger)
    {
    }
}
