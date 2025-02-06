using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volcanion.Core.Common.Abstractions;
using Volcanion.Core.Common.Implementations;
using Volcanion.Core.Common.Models.Redis;

namespace Volcanion.Core.Common;

/// <summary>
/// ProviderRegister
/// </summary>
public static class ProviderRegister
{
    /// <summary>
    /// AddProviderServices
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterProviders(this IServiceCollection services)
    {
        services.AddTransient<IConfigProvider, ConfigProvider>();
        services.AddTransient<ICookieProvider, CookieProvider>();
        services.AddTransient<IHashProvider, HashProvider>();
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddTransient<ISafeThreadProvider, SafeThreadProvider>();
        services.AddTransient<IStringProvider, StringProvider>();
        services.AddTransient<IMemCacheProvider, MemCacheProvider>();
    }

    /// <summary>
    /// AddRedisCacheService <br/>
    /// Add Redis cache service to the service collection <br/>
    /// This method will init Redis cache service and add it to the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddRedisCacheService(this IServiceCollection services, IConfiguration configuration)
    {
        // Get Redis configuration
        var redisConfig = configuration.GetSection("Redis");

        // Add Redis cache service
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfig["Hostname"];
            options.InstanceName = redisConfig["InstanceName"];
        });

        // Add Redis cache provider
        services.AddTransient<IRedisCacheProvider, RedisCacheProvider>();
        // Add Redis options
        services.Configure<RedisOptions>(configuration.GetSection("Redis"));
        // Return services
        return services;
    }
}
