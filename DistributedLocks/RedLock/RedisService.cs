using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace RedLock;

public class RedisService
{
    private IDatabase _redis;
    private RedLockFactory _factory;
    
    public RedisService()
    {
        InitRedis();
    }

    public Task<IRedLock> CreateLockAsync()
    {
        var resource = "lock-resource";
        var expiry = TimeSpan.FromSeconds(30);
        
        return _factory.CreateLockAsync(resource, expiry);
    }

    public async Task<string> GetLockValueAsync()
    {
        var key = "redlock:lock-resource";
        
        var value = await _redis.StringGetAsync(key);
        
        return value.ToString();
    }
    
    public async Task SetLockValueAsync(string value)
    {
        var key = "redlock:lock-resource";
        
        await _redis.StringSetAsync(key, value);
    }
    
    private void InitRedis()
    {
        var multiplexer = ConnectionMultiplexer.Connect("localhost:6379");
        _redis = multiplexer.GetDatabase();
        
        _factory = RedLockFactory.Create(new List<RedLockMultiplexer> { multiplexer });
    }
}