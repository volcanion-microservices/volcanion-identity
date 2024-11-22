using Microsoft.Extensions.Logging;
using Volcanion.Core.ServiceHandler.Implementations;
using Volcanion.Identity.Infrastructure.Abstractions;
using Volcanion.Identity.Models.Entities;
using Volcanion.Identity.ServiceHandler.Abstractions;

namespace Volcanion.Identity.ServiceHandler.Implementations;

/// <inheritdoc />
internal class RoleService : BaseService<Role, IRoleRepository>, IRoleService
{
    public RoleService(IRoleRepository repository, ILogger<BaseService<Role, IRoleRepository>> logger) : base(repository, logger)
    {
    }
}
