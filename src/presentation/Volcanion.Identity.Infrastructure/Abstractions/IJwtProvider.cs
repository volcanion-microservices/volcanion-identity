using Volcanion.Core.Models.Enums;
using Volcanion.Core.Models.Jwt;
using Volcanion.Identity.Models.Entities;

namespace Volcanion.Identity.Infrastructure.Abstractions;

/// <summary>
/// IJwtProvider
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    /// GenerateJwt
    /// </summary>
    /// <param name="account"></param>
    /// <param name="audience"></param>
    /// <param name="issuer"></param>
    /// <param name="allowedOrigins"></param>
    /// <param name="groupAccess"></param>
    /// <param name="resourceAccess"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string GenerateJwt(Account account, string audience, string issuer, List<string> allowedOrigins, List<string> groupAccess, ResourceAccess resourceAccess, JwtType type);

    /// <summary>
    /// ValidateJwt
    /// </summary>
    /// <param name="jwt"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public bool ValidateJwt(string jwt, JwtType type);

    /// <summary>
    /// DecodeJwt
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public (JwtHeader? header, JwtPayload? payload) DecodeJwt(string token);

    /// <summary>
    /// SplitJwt
    /// </summary>
    /// <param name="jwt"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public (string signature, string headerPayload) SplitJwt(string jwt);
}
