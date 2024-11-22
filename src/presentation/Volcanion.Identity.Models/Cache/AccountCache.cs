using Volcanion.Identity.Models.Entities;

namespace Volcanion.Identity.Models.Cache;

/// <summary>
/// AccountCache
/// </summary>
public class AccountCache
{
    /// <summary>
    /// AccountData
    /// </summary>
    public Account AccountData { get; set; } = null!;

    /// <summary>
    /// RefreshToken
    /// </summary>
    public string RefreshToken { get; set; } = null!;
}
