using Microsoft.AspNetCore.Http;
using Volcanion.Core.Common.Abstractions;

namespace Volcanion.Core.Common.Implementations;

/// <inheritdoc/>
/// <summary>
/// Constructor
/// </summary>
/// <param name="httpContextAccessor"></param>
public class CookieProvider(IHttpContextAccessor httpContextAccessor) : ICookieProvider
{
    /// <inheritdoc/>
    public void Set(string key, string value, int? expireTime = 3600)
    {
        expireTime ??= 3600;

        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddMinutes(expireTime.Value)
        };

        httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
    }

    /// <inheritdoc/>
    public string Get(string key)
    {
        return httpContextAccessor.HttpContext.Request.Cookies[key]!;
    }

    /// <inheritdoc/>
    public void Remove(string key)
    {
        httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
    }

    /// <inheritdoc/>
    public void RemoveAlls()
    {
        foreach (string key in httpContextAccessor.HttpContext.Request.Cookies.Keys)
        {
            Remove(key);
        }
    }
}
