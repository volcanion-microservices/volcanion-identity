using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Volcanion.Core.Common.Abstractions;
using Volcanion.Core.Common.Models.Redis;

namespace Volcanion.Core.Common.Implementations;

/// <inheritdoc/>
/// <summary>
/// Constructor
/// </summary>
/// <param name="cache"></param>
/// <param name="redisOptionsMonitor"></param>
public class RedisCacheProvider(IDistributedCache cache, IOptionsMonitor<RedisOptions> redisOptionsMonitor) : IRedisCacheProvider
{
    /// <inheritdoc/>
    public async Task<T?> GetCacheAsync<T>(string key)
    {
        var jsonData = await cache.GetStringAsync(key);

        if (jsonData is null)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(jsonData);
    }

    /// <inheritdoc/>
    public async Task<T?> SetCacheAsync<T>(string key, T value)
    {
        var optionsCache = redisOptionsMonitor.CurrentValue;
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = optionsCache.AbsoluteExpireTime,
            SlidingExpiration = optionsCache.SlidingExpireTime
        };

        var jsonData = JsonSerializer.Serialize(value);

        await cache.SetStringAsync(key, jsonData, options);

        return value;
    }
}
