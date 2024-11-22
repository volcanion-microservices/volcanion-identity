using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volcanion.Core.Common.Abstractions;
using Volcanion.Core.Models.Enums;
using Volcanion.Core.Models.Jwt;
using Volcanion.Core.ServiceHandler.Implementations;
using Volcanion.Identity.Infrastructure.Abstractions;
using Volcanion.Identity.Models.Entities;
using Volcanion.Identity.Models.Request;
using Volcanion.Identity.Models.Response;
using Volcanion.Identity.Models.Setting;
using Volcanion.Identity.ServiceHandler.Abstractions;

namespace Volcanion.Identity.ServiceHandler.Implementations;

/// <inheritdoc />
internal class AccountService : BaseService<Account, IAccountRepository>, IAccountService
{
    /// <summary>
    /// ICacheProvider
    /// </summary>
    private readonly IRedisCacheProvider _redisCacheProvider;

    /// <summary>
    /// IJwtProvider
    /// </summary>
    private readonly IJwtProvider _jwtProvider;

    /// <summary>
    /// IHahsProvider instance
    /// </summary>
    private readonly IHashProvider _hashProvider;

    /// <summary>
    /// AllowedOrigin
    /// </summary>
    private string[] AllowedOrigin { get; set; }

    /// <summary>
    /// Audience
    /// </summary>
    private string Audience { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="logger"></param>
    /// <param name="hashProvider"></param>
    public AccountService(IAccountRepository repository, ILogger<BaseService<Account, IAccountRepository>> logger, IHashProvider hashProvider, IConfigProvider configProvider, IOptions<AppSettings> options) : base(repository, logger)
    {
        _hashProvider = hashProvider;
        AllowedOrigin = options.Value.AllowedOrigins;
        Audience = options.Value.Audience;
    }

    /// <inheritdoc />
    public async Task<AccountResponse?> Login(AccountLogin account)
    {
        try
        {
            // Find account by email
            var accountFind = await _repository.GetAccountByEmail(account.LoginName);
            // If account not found, return null
            if (accountFind == null) return null;
            // Verify password
            if (_hashProvider.VerifyPassword(accountFind.Password, account.Password)) return null;

            // TODO: implement group access
            var groupAccess = new List<string>();
            // TODO: implement resource access
            var resourceAccess = new ResourceAccess();

            // Generate access token
            var refreshToken = "";
            var accessToken = _jwtProvider.GenerateJwt(accountFind, Audience, account.Issuer, AllowedOrigin.ToList(), groupAccess, resourceAccess, JwtType.AccessToken);

            // If remember me is true, generate refresh token
            if (account.RememberMe)
            {
                refreshToken = _jwtProvider.GenerateJwt(accountFind, Audience, account.Issuer, AllowedOrigin.ToList(), groupAccess, resourceAccess, JwtType.RefreshToken);
            }

            return new AccountResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Account = accountFind
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw new Exception(ex.Message);
        }
    }

    /// <inheritdoc />
    public async Task<AccountResponse> RefreshToken(TokenRequest request)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw new Exception(ex.Message);
        }
    }

    /// <inheritdoc />
    public async Task<AccountResponse> Register(AccountRegister account)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw new Exception(ex.Message);
        }
    }
}
