using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volcanion.Core.Presentation.Controllers;
using Volcanion.Identity.Models.Request;
using Volcanion.Identity.ServiceHandler.Abstractions;

namespace Volcanion.Identity.Presentation.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[AllowAnonymous]
public class AuthController : BaseController
{
    /// <summary>
    /// ILogger instance
    /// </summary>
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// IAccountService instance
    /// </summary>
    private readonly IAccountService _accountService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="accountService"></param>
    public AuthController(ILogger<AuthController> logger, IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    /// <summary>
    /// Register
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(AccountRegister account)
    {
        try
        {
            var result = await _accountService.Register(account);
            return Ok(SuccessData(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(5, ex, "Something went wrong...");
            return Ok(ErrorMessage(ex.Message));
        }
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(AccountLogin account)
    {
        try
        {
            var result = await _accountService.Login(account);
            return Ok(SuccessData(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(5, ex, "Something went wrong...");
            return Ok(ErrorMessage(ex.Message));
        }
    }

    /// <summary>
    /// RefreshToken
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenRequest request)
    {
        try
        {
            var result = await _accountService.RefreshToken(request);
            return Ok(SuccessData(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(5, ex, "Something went wrong...");
            return Ok(ErrorMessage(ex.Message));
        }
    }
}
