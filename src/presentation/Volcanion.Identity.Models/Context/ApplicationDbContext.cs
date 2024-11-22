using Microsoft.EntityFrameworkCore;
using Volcanion.Identity.Models.Entities;

namespace Volcanion.Identity.Models.Context;

/// <summary>
/// ApplicationDbContext
/// </summary>
/// <param name="options"></param>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Account
    /// </summary>
    public DbSet<Account> Account { get; set; }

    /// <summary>
    /// Roles
    /// </summary>
    public DbSet<Role> Roles { get; set; }

    /// <summary>
    /// Permissions
    /// </summary>
    public DbSet<Permission> Permissions { get; set; }

    /// <summary>
    /// RolePermissions
    /// </summary>
    public DbSet<RolePermission> RolePermissions { get; set; }

    /// <summary>
    /// GrantPermissions
    /// </summary>
    public DbSet<GrantPermission> GrantPermissions { get; set; }

    /// <summary>
    /// OnModelCreating
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call base OnModelCreating method
        base.OnModelCreating(modelBuilder);

        // Configure the Role entity
        modelBuilder.Entity<Role>(r =>
        {
            // A Role can have many RolePermissions
            r.HasMany(role => role.RolePermissions)
            // Each RolePermission is associated with one Role
            .WithOne(p => p.Role)
            // The foreign key in RolePermission is PermissionId
            .HasForeignKey(p => p.PermissionId);
        });

        // Configure the Permission entity
        modelBuilder.Entity<Permission>(p =>
        {
            // A Permission can have many RolePermissions
            p.HasMany(permission => permission.RolePermissions)
            // Each RolePermission is associated with one Permission
            .WithOne(p => p.Permission)
            // The foreign key in RolePermission is PermissionId
            .HasForeignKey(p => p.PermissionId);
        });

        // Configure the GrantPermission entity
        modelBuilder.Entity<GrantPermission>(gp =>
        {
            // A GrantPermission can have many RolePermissions
            gp.HasMany(grant => grant.RolePermissions)
            // Each RolePermission is associated with one GrantPermission
            .WithOne(rp => rp.GrantPermission)
            // The foreign key in RolePermission is GrantPermissionId
            .HasForeignKey(rp => rp.GrantPermissionId);
        });

        // Configure the Account entity
        modelBuilder.Entity<Account>(ac =>
        {
            // An Account can have many GrantPermissions
            ac.HasMany(account => account.GrantPermissions)
            // Each GrantPermission is associated with one Account
            .WithOne(gp => gp.Account)
            // The foreign key in GrantPermission is AccountId
            .HasForeignKey(gp => gp.AccountId);
        });
    }
}
