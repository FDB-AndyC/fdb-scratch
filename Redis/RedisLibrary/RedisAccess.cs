namespace RedisLibrary
{
    using Serenity.Abstractions;

    public interface IRedisAccess
    {
        string GetStringSetting(string settingName);
    }

    public class RedisAccess : IRedisAccess
    {
        private readonly IDistributedCache _cache;

        public RedisAccess(IDistributedCache cache)
        {
            _cache = cache;
        }

        public string GetStringSetting(string settingName)
        {
            return _cache.Get<string>(settingName);
        }
    }
}
