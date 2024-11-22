using Microsoft.Extensions.Logging;
using Volcanion.Core.ServiceHandler.Implementations;
using Volcanion.Identity.Infrastructure.Abstractions;
using Volcanion.Identity.Models.Entities;
using Volcanion.Identity.ServiceHandler.Abstractions;

namespace Volcanion.Identity.ServiceHandler.Implementations;

/// <inheritdoc />
internal class GrantPermissionService : BaseService<GrantPermission, IGrantPermissionRepository>, IGrantPermissionService
{
    public GrantPermissionService(IGrantPermissionRepository repository, ILogger<BaseService<GrantPermission, IGrantPermissionRepository>> logger) : base(repository, logger)
    {
    }
}
