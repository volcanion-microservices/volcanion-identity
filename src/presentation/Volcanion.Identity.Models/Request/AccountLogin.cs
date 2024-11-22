namespace Volcanion.Identity.Models.Request;

/// <summary>
/// AccountLogin
/// </summary>
public class AccountLogin
{
    /// <summary>
    /// LoginName
    /// </summary>
    public string LoginName { get; set; } = null!;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// IpAddress
    /// </summary>
    public string IpAddress { get; set; } = null!;

    /// <summary>
    /// RememberMe
    /// </summary>
    public bool RememberMe { get; set; } = false;

    /// <summary>
    /// Issuer
    /// </summary>
    public string Issuer { get; set; } = null!;
}
