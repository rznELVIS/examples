using StackExchange.Redis;

namespace RedLock;

public class RedisService
{
    private IDatabase _redis;
    
    public RedisService()
    {
        InitRedis();
    }

    private void InitRedis()
    {
        var multiplexer = ConnectionMultiplexer.Connect("localhost:6379");
        _redis = multiplexer.GetDatabase();
    }
}