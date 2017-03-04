namespace JOS.WorldStatus.Features.Shared
{
	public class MetroSettings
	{
		public RealTimeDepartures RealTimeDepartures { get; set; }
	}

	public class RealTimeDepartures
	{
		public string ApiKey { get; set; }
		public string BaseUrl { get; set; }
	}
}