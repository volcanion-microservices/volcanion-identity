using Microsoft.Extensions.Logging;
using Volcanion.Core.Infrastructure.Implementations;
using Volcanion.Identity.Infrastructure.Abstractions;
using Volcanion.Identity.Models.Context;
using Volcanion.Identity.Models.Entities;

namespace Volcanion.Identity.Infrastructure.Implementations;

internal class PermissionRepository(ApplicationDbContext context, ILogger<BaseRepository<Permission, ApplicationDbContext>> logger) : BaseRepository<Permission, ApplicationDbContext>(context, logger), IPermissionRepository
{
}
