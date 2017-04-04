using System;

namespace JOS.WorldStatus.Features.Shared
{
	public interface ICache
	{
		T Get<T>(string key);
		void Save<T>(string key, T value, DateTime expires);
		void Save<T>(string key, T value, TimeSpan absoluteExpirationRelativeToNow);
		void Remove(string key);
	}
}