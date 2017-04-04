using System;
using System.Threading.Tasks;
using JOS.WorldStatus.Features.Shared;
using JOS.WorldStatus.Features.Shared.Dtos.TrafikLab;

namespace JOS.WorldStatus.Features.Metro
{
	public class CachedHttpRealTimeMetroInfoQuery : IGetRealTimeMetroInfoQuery
	{
		private readonly ICache _cache;
		private readonly HttpGetRealTimeMetroInfoQuery _httpGetRealTimeMetroInfoQuery;

		public CachedHttpRealTimeMetroInfoQuery(
			HttpGetRealTimeMetroInfoQuery httpGetRealTimeMetroInfoQuery,
			ICache cache
		)
		{
			_httpGetRealTimeMetroInfoQuery = httpGetRealTimeMetroInfoQuery ?? throw new ArgumentNullException(nameof(httpGetRealTimeMetroInfoQuery));
			_cache = cache ?? throw new ArgumentNullException(nameof(cache));
		}

		public async Task<TrafikLabResponseDto<DepartureDto>> Execute(int siteId)
		{
			var cacheKey = $"{nameof(HttpGetRealTimeMetroInfoQuery)}-{siteId}";
			var fromCache = this._cache.Get<TrafikLabResponseDto<DepartureDto>>(cacheKey);
			if (fromCache != null)
			{
				return fromCache;
			}

			var response = await _httpGetRealTimeMetroInfoQuery.Execute(siteId);
			_cache.Save(cacheKey, response, TimeSpan.FromMinutes(1));
			return response;
		}
	}
}
