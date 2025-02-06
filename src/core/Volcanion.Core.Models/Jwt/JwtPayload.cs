using Newtonsoft.Json;

namespace Volcanion.Core.Models.Jwt;

/// <summary>
/// JwtPayload
/// </summary>
public class JwtPayload<T>
{
    /// <summary>
    /// Expiration
    /// Token expiration time (timestamp).
    /// </summary>
    [JsonProperty("exp")]
    public long Expiration { get; set; }

    /// <summary>
    /// IssuedAt <br/>
    /// Token creation time.
    /// </summary>
    [JsonProperty("iat")]
    public long IssuedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

    /// <summary>
    /// TokenId <br/>
    /// The ID of the token, to distinguish between tokens.
    /// </summary>
    [JsonProperty("jti")]
    public string TokenId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Issuer <br/>
    /// Token issuer, here is the URL of the Identity server.
    /// </summary>
    [JsonProperty("iss")]
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Subject <br/>
    /// Subject, the title of the token, or the purpose of the token.
    /// </summary>
    [JsonProperty("sub")]
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Audience <br/>
    /// Audience, service that the token is intended for.
    /// </summary>
    [JsonProperty("aud")]
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// SessionId
    /// </summary>
    [JsonProperty("sid")]
    public string SessionId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// AllowedOrigins
    /// </summary>
    [JsonProperty("allowed_origins")]
    public List<string> AllowedOrigins { get; set; } = [];

    /// <summary>
    /// ResourceAccess <br/>
    /// Role information, permission information, etc...
    /// </summary>
    [JsonProperty("resource_access")]
    public ResourceAccess ResourceAccess { get; set; } = new();

    /// <summary>
    /// GroupAccess <br/>
    /// Group information, etc...
    /// </summary>
    [JsonProperty("group_access")]
    public List<string> GroupAccess { get; set; } = [];

    /// <summary>
    /// ResourceData <br/>
    /// Account information, user information, etc...
    /// </summary>
    [JsonProperty("resource_data")]
    public T? ResourceData { get; set; }

    /// <summary>
    /// Additional properties to convert UNIX timestamp to DateTime
    /// </summary>
    public DateTime ExpirationDate => DateTimeOffset.FromUnixTimeSeconds(Expiration).UtcDateTime;

    /// <summary>
    /// Additional properties to convert UNIX timestamp to DateTime
    /// </summary>
    public DateTime IssuedAtDate => DateTimeOffset.FromUnixTimeSeconds(IssuedAt).UtcDateTime;
}

/// <summary>
/// ResourceAccess
/// </summary>
public class ResourceAccess
{
    /// <summary>
    /// RoleAccess
    /// </summary>
    [JsonProperty("role_access")]
    public RoleAccess RoleAccess { get; set; } = new();
}

/// <summary>
/// RoleAccess
/// </summary>
public class RoleAccess
{
    /// <summary>
    /// Roles
    /// </summary>
    [JsonProperty("roles")]
    public List<string> Roles { get; set; } = [];
}
