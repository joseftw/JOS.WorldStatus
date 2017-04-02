using System;

namespace JOS.WorldStatus.Features.Shared
{
	public static class DateTimeExtensions
	{
		public static double ToUnixTimestamp(this DateTime dateTime)
		{
			return (dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
		}
	}
}
