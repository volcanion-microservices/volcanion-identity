using Volcanion.Core.Models.Enums;
using Volcanion.Core.Models.Jwt;

namespace Volcanion.Core.Common.Abstractions;

/// <summary>
/// IJwtProvider
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    /// GenerateJwt
    /// </summary>
    /// <param name="data"></param>
    /// <param name="audience"></param>
    /// <param name="issuer"></param>
    /// <param name="allowedOrigins"></param>
    /// <param name="groupAccess"></param>
    /// <param name="resourceAccess"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string GenerateJwt<T>(T data, string audience, string issuer, List<string> allowedOrigins, List<string> groupAccess, ResourceAccess resourceAccess, JwtType type);

    /// <summary>
    /// ValidateJwt
    /// </summary>
    /// <param name="jwt"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public bool ValidateJwt<T>(string jwt, JwtType type);

    /// <summary>
    /// DecodeJwt
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public (JwtHeader? header, JwtPayload<T>? payload) DecodeJwt<T>(string token);

    /// <summary>
    /// SplitJwt
    /// </summary>
    /// <param name="jwt"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public (string signature, string headerPayload) SplitJwt(string jwt);
}
