using Microsoft.Extensions.Caching.Memory;
using Volcanion.Core.Common.Abstractions;

namespace Volcanion.Core.Common.Implementations;

/// <inheritdoc/>
/// <summary>
/// Constructor
/// </summary>
/// <param name="memoryCache"></param>
public class MemCacheProvider(IMemoryCache memoryCache) : IMemCacheProvider
{
    /// <inheritdoc/>
    public object Get(string key)
    {
        memoryCache.TryGetValue(key, out var result);
        return result ?? false;
    }

    /// <inheritdoc/>
    public object Set(string key, object value)
    {
        return memoryCache.Set(key, value);
    }

    /// <inheritdoc/>
    public object Set(string key, object value, DateTimeOffset absoluteExpiration)
    {
        return memoryCache.Set(key, value, absoluteExpiration);
    }

    /// <inheritdoc/>
    public void Delete(string key)
    {
        memoryCache.Remove(key);
    }
}
