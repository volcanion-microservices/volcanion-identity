using Microsoft.Extensions.Logging;
using Volcanion.Core.Infrastructure.Implementations;
using Volcanion.Identity.Infrastructure.Abstractions;
using Volcanion.Identity.Models.Context;
using Volcanion.Identity.Models.Entities;

namespace Volcanion.Identity.Infrastructure.Implementations;

internal class GrantPermissionRepository(ApplicationDbContext context, ILogger<BaseRepository<GrantPermission, ApplicationDbContext>> logger) : BaseRepository<GrantPermission, ApplicationDbContext>(context, logger), IGrantPermissionRepository
{
}
