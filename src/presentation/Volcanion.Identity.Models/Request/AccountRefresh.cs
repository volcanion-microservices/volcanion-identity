namespace Volcanion.Identity.Models.Request;

/// <summary>
/// AccountRefresh
/// </summary>
public class AccountRefresh
{
    /// <summary>
    /// LoginName
    /// </summary>
    public string LoginName { get; set; } = null!;

    /// <summary>
    /// AccessToken
    /// </summary>
    public string AccessToken { get; set; } = null!;

    /// <summary>
    /// RefreshToken
    /// </summary>
    public string RefreshToken { get; set; } = null!;
}
