﻿using Microsoft.Extensions.Configuration;
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
        services.AddTransient<ISafeThreadProvider, SafeThreadProvider>();
        services.AddTransient<IStringProvider, StringProvider>();
        services.AddTransient<IMemCacheProvider, MemCacheProvider>();
    }

    public static IServiceCollection AddRedisCacheService(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConfig = configuration.GetSection("Redis");

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfig["Hostname"];
            options.InstanceName = redisConfig["InstanceName"];
        });

        services.AddTransient<IRedisCacheProvider, RedisCacheProvider>();

        services.Configure<RedisOptions>(configuration.GetSection("Redis"));

        return services;
    }
}
