using System;
using Microsoft.Extensions.Caching.Memory;

namespace JOS.WorldStatus.Features.Shared
{
	public class MemoryCache : ICache
	{
		private readonly IMemoryCache _memoryCache;

		public MemoryCache(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
		}

		public T Get<T>(string key)
		{
			return _memoryCache.Get<T>(key);
		}

		public void Save<T>(string key, T value, DateTime expires)
		{
			_memoryCache.Set(key, value, expires);
		}

		public void Save<T>(string key, T value, TimeSpan absoluteExpirationRelativeToNow)
		{
			_memoryCache.Set(key, value, absoluteExpirationRelativeToNow);
		}

		public void Remove(string key)
		{
			_memoryCache.Remove(key);
		}
	}
}
