using Volcanion.Core.Models.Entities;

namespace Volcanion.Identity.Models.Entities;

/// <summary>
/// Role
/// </summary>
public class Role : BaseEntity
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// RolePermissions
    /// </summary>
    public ICollection<RolePermission> RolePermissions { get; set; }
}
