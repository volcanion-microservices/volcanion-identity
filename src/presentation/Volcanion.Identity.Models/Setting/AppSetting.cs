namespace Volcanion.Identity.Models.Setting;

/// <summary>
/// AppSettings
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Audience
    /// </summary>
    public string[] AllowedOrigins { get; set; }

    /// <summary>
    /// Issuer
    /// </summary>
    public string Audience { get; set; }
}
