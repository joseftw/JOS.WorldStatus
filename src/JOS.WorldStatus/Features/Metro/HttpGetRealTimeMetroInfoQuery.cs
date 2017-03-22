using System;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using JOS.WorldStatus.Features.Shared;
using JOS.WorldStatus.Features.Shared.Dtos.TrafikLab;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace JOS.WorldStatus.Features.Metro
{
	public class HttpGetRealTimeMetroInfoQuery : IGetRealTimeMetroInfoQuery
	{
		private readonly MetroSettings _metroSettings;

		private static readonly HttpClient Client = new HttpClient
		{
			Timeout = TimeSpan.FromSeconds(5)
		};

		public HttpGetRealTimeMetroInfoQuery(IOptions<MetroSettings> metroSettings)
		{
			_metroSettings = metroSettings.Value;
		}

		public async Task<TrafikLabResponseDto<DepartureDto>> Execute(int siteId)
		{
			var fullUrl = new Url(this._metroSettings.RealTimeDepartures.BaseUrl);
			fullUrl.QueryParams.Add("key", this._metroSettings.RealTimeDepartures.ApiKey);
			fullUrl.QueryParams.Add("siteid", siteId);
			fullUrl.QueryParams.Add("timewindow", 60);
			var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
			var response = await Client.SendAsync(request);
			var responseBody = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<TrafikLabResponseDto<DepartureDto>>(responseBody);
			return result;
		}
	}
}