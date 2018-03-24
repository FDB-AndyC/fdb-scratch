
namespace RedisTest01
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Serenity.Abstractions;
    using Serenity.Caching;
    using System;
    using RedisLibrary;

    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDistributedRedisCache(options =>
                    {
                        options.Configuration = "localhost:6379";
                    })
                .AddScoped<IRedisAccess, RedisAccess>()
                .BuildServiceProvider();

            Console.WriteLine("Hello World!");

            var access = serviceProvider.GetService<IRedisAccess>();
            Console.WriteLine($"Message: {access.GetStringSetting("message")}");
        }
        
    }
}
