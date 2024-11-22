using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Volcanion.Core.Common.Abstractions;
using Volcanion.Core.Models.Enums;
using Volcanion.Core.Models.Jwt;
using Volcanion.Identity.Infrastructure.Abstractions;
using Volcanion.Identity.Models.Entities;

namespace Volcanion.Identity.Infrastructure.Implementations;

/// <inheritdoc/>
internal class JwtProvider : IJwtProvider
{
    /// <summary>
    /// IStringProvider instance
    /// </summary>
    private readonly IStringProvider _stringProvider;

    /// <summary>
    /// IRedisCacheProvider instance
    /// </summary>
    private readonly IRedisCacheProvider _redisCacheProvider;

    /// <summary>
    /// IHahsProvider instance
    /// </summary>
    private readonly IHashProvider _hashProvider;

    /// <summary>
    /// IConfigProvider instance
    /// </summary>
    private readonly IConfigProvider _configProvider;

    /// <summary>
    /// ILogger instance
    /// </summary>
    private readonly ILogger<JwtProvider> _logger;

    /// <summary>
    /// PrivateKeyFilePath
    /// </summary>
    private string PrivateKeyFilePath { get; set; }

    /// <summary>
    /// PublicKeyFilePath
    /// </summary>
    private string PublicKeyFilePath { get; set; }

    /// <summary>
    /// AccessTokenExpiredTime
    /// </summary>
    private string AccessTokenExpiredTime { get; set; }

    /// <summary>
    /// RefreshTokenExpiredTime
    /// </summary>
    private string RefreshTokenExpiredTime { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="stringProvider"></param>
    /// <param name="redisCacheProvider"></param>
    /// <param name="hashProvider"></param>
    /// <param name="configProvider"></param>
    /// <param name="logger"></param>
    public JwtProvider(IStringProvider stringProvider, IRedisCacheProvider redisCacheProvider, IHashProvider hashProvider, IConfigProvider configProvider, ILogger<JwtProvider> logger)
    {
        _stringProvider = stringProvider;
        _redisCacheProvider = redisCacheProvider;
        _hashProvider = hashProvider;
        _configProvider = configProvider;
        _logger = logger;
        PrivateKeyFilePath = _configProvider.GetConfigString("PrivateKeyFilePath");
        PublicKeyFilePath = _configProvider.GetConfigString("PublicKeyFilePath");
        AccessTokenExpiredTime = _configProvider.GetConfigString("AccessTokenExpiredTime");
        RefreshTokenExpiredTime = _configProvider.GetConfigString("RefreshTokenExpiredTime");
    }

    /// <inheritdoc/>
    public (JwtHeader? header, JwtPayload? payload) DecodeJwt(string token)
    {
        try
        {
            // Split the jwt
            var jwtSplit = token.Split('.');
            // Check if the jwt is not valid
            if (jwtSplit.Length != 3) throw new Exception("Jwt is not valid.");

            // Decode the jwt
            var headerJsonStr = _hashProvider.Base64Decode(jwtSplit[0]);
            var payloadJsonStr = _hashProvider.Base64Decode(jwtSplit[1]);

            // Deserialize the jwt
            var header = JsonConvert.DeserializeObject<JwtHeader>(headerJsonStr);
            var payload = JsonConvert.DeserializeObject<JwtPayload>(payloadJsonStr);
            
            // Return the header and payload
            return (header, payload);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw new Exception(ex.Message);
        }
    }

    /// <inheritdoc/>
    public string GenerateJwt(Account account, string audience, string issuer, List<string> allowedOrigins, List<string> groupAccess, ResourceAccess resourceAccess, JwtType type)
    {
        try
        {
            // Generate header
            var header = new JwtHeader
            {
                Algorithm = "HS512",
                Type = "JWT"
            };

            // Generate expiration time
            var datetimeOffsetData = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var datetimeOffsetDataCompareStr = "";

            if (type == JwtType.AccessToken)
            {
                // Check if the access token expired time is empty and set it to 10 minutes
                if (string.IsNullOrEmpty(AccessTokenExpiredTime)) AccessTokenExpiredTime = "10m";
                datetimeOffsetDataCompareStr = AccessTokenExpiredTime;
            }
            else
            {
                // Check if the refresh token expired time is empty and set it to 30 days
                if (string.IsNullOrEmpty(RefreshTokenExpiredTime)) RefreshTokenExpiredTime = "30d";
                datetimeOffsetDataCompareStr = RefreshTokenExpiredTime;
            }

            // Convert the expiration time to Unix time
            var datetimeOffsetDataCompare = _stringProvider.GenerateDateTimeOffsetFromString(datetimeOffsetDataCompareStr).ToUnixTimeSeconds();

            // Generate payload
            var payload = new JwtPayload
            {
                Audience = audience,
                Issuer = issuer,
                AllowedOrigins = allowedOrigins,
                GroupAccess = groupAccess,
                Expiration = datetimeOffsetDataCompare,
                ResourceAccess = resourceAccess,
                Email = account.Email,
                Name = account.Fullname
            };

            // Generate the jwt
            var jwtHeader = JsonConvert.SerializeObject(header);
            var jwtPayload = JsonConvert.SerializeObject(payload);
            var jwtHeaderBase64 = _hashProvider.Base64Encode(jwtHeader);
            var jwtPayloadBase64 = _hashProvider.Base64Encode(jwtPayload);
            var jwtSignature = _hashProvider.HashSHA512($"{jwtHeaderBase64}.{jwtPayloadBase64}", PrivateKeyFilePath);
            return $"{jwtHeaderBase64}.{jwtPayloadBase64}.{jwtSignature}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw new Exception(ex.Message);
        }
    }

    /// <inheritdoc/>
    public bool ValidateJwt(string jwt, JwtType type)
    {
        try
        {
            // Validate the jwt
            // Check if the jwt is empty
            if (string.IsNullOrEmpty(jwt))
            {
                throw new Exception("Jwt is empty.");
            }
            
            // Check if the public key file path is empty
            if (string.IsNullOrEmpty(PublicKeyFilePath))
            {
                throw new Exception("Public key file path is not set.");
            }

            var signature = SplitJwt(jwt).signature;
            var headerPayload = SplitJwt(jwt).headerPayload;

            if (!_hashProvider.VerifySignature(jwt, headerPayload, PublicKeyFilePath))
            {
                throw new Exception("Jwt signature is not valid.");
            }

            var datetimeOffsetData = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var datetimeOffsetDataCompareStr = "";

            if (type == JwtType.AccessToken)
            {
                // Check if the access token expired time is empty and set it to 10 minutes
                if (string.IsNullOrEmpty(AccessTokenExpiredTime)) AccessTokenExpiredTime = "10m";
                datetimeOffsetDataCompareStr = AccessTokenExpiredTime;
            }
            else
            {
                // Check if the refresh token expired time is empty and set it to 30 days
                if (string.IsNullOrEmpty(RefreshTokenExpiredTime)) RefreshTokenExpiredTime = "30d";
                datetimeOffsetDataCompareStr = RefreshTokenExpiredTime;
            }

            var datetimeOffsetDataCompare = _stringProvider.GenerateDateTimeOffsetFromString(datetimeOffsetDataCompareStr).ToUnixTimeSeconds();
            var payload = DecodeJwt(headerPayload).payload;

            // Check if the jwt is expired
            if (datetimeOffsetDataCompare < payload!.Expiration) throw new Exception("Jwt is expired.");
            var sessionId = payload.SessionId;
            var cacheSessionId = _redisCacheProvider.GetCacheAsync<string>(sessionId).Result;

            if (cacheSessionId.Equals("Expired")) throw new Exception("Session is expired.");

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw new Exception(ex.Message);
        }
    }

    /// <inheritdoc/>
    public (string signature, string headerPayload) SplitJwt(string jwt)
    {
        try
        {
            // Split the jwt
            var jwtSplit = jwt.Split('.');
            // Check if the jwt is not valid
            if (jwtSplit.Length != 3) throw new Exception("Jwt is not valid.");
            // Return the signature and header payload
            return (jwtSplit[2], $"{jwtSplit[0]}.{jwtSplit[1]}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw new Exception(ex.Message);
        }
    }
}
