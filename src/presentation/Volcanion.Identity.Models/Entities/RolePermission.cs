using Volcanion.Core.Models.Entities;

namespace Volcanion.Identity.Models.Entities;

/// <summary>
/// RolePermission
/// </summary>
public class RolePermission : BaseEntity
{
    /// <summary>
    /// RoleId
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// PermissionId
    /// </summary>
    public Guid PermissionId { get; set; }

    /// <summary>
    /// GrantPermissionId
    /// </summary>
    public Guid GrantPermissionId { get; set; }

    /// <summary>
    /// Role
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Permission
    /// </summary>
    public Permission Permission { get; set; }

    /// <summary>
    /// GrantPermission
    /// </summary>
    public GrantPermission GrantPermission { get; set; }
}
