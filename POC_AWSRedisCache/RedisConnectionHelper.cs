using StackExchange.Redis;
using System;

namespace POC_AWSRedisCache
{
    public class RedisConnectionHelper
    {
        static readonly ConfigurationOptions options = new ConfigurationOptions
        {//"redis-13990.c212.ap-south-1-1.ec2.cloud.redislabs.com:13990" 
            EndPoints = { "smartpo-redis-dev.q0mgbe.ng.0001.usw2.cache.amazonaws.com:6379" },
            //Password = "ambujRedis123",
            Ssl = false,
            AbortOnConnectFail = false,
            DefaultDatabase = 0
            
        };
        static RedisConnectionHelper()
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(options);
            });
        }

        private static readonly Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
