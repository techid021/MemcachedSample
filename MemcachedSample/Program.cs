
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Microsoft.Extensions.Logging;



string key = "Age";

// Create a logger factory configured to log to the console.
using var loggerFactory = LoggerFactory.Create(builder =>
    builder.AddConsole().SetMinimumLevel(LogLevel.Warning));

ILogger logger = loggerFactory.CreateLogger<Program>();

// Configure the Memcached client.
var config = new MemcachedClientConfiguration(loggerFactory, new MemcachedClientOptions());
config.AddServer("localhost", 11211); // Replace with your Memcached server address and port.

// Create the Memcached client.
MemcachedClient myCache = new MemcachedClient(loggerFactory, config);

// Set a value in the cache.
Console.WriteLine($"Setting - Key:{key}, Value:31");
await myCache.SetAsync(key, 31, 10); // Expires after 10 seconds.

// Wait for a second to allow the cache to be populated.
Console.WriteLine("\nWaiting a second...");
await Task.Delay(1000);

// Get the value from the cache.
int number = myCache.Get<int>(key);
Console.WriteLine($"Getting - Key:{key}, Value:{number}");