using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace JOS.WorldStatus.Features.Metro
{
	public static class MetroHelpers
	{
		private static readonly Regex TimeRemainingRegex = new Regex(@"(\d{1,2})( min)");
		private static readonly Regex TimeRegex = new Regex("[0-2]{1}[0-9]{1}:[0-5]{1}[0-9]{1}");
		/// <summary>
		/// Used as a backup for the 'Röda linjen', they don't have TimeTabled
		/// or Expected time set yet, so we calculate it based on the DisplayTime.
		/// </summary>
		/// <returns></returns>
		public static DateTime? TimeTabled(string displayTime)
		{
			if (displayTime == null)
			{
				return null;
			}

			var now = DateTime.UtcNow;
			if (TimeRemainingRegex.IsMatch(displayTime))
			{
				var minute = int.Parse(displayTime.Length == 6 ? displayTime.Substring(0,2) : displayTime.Substring(0,1));
				return now.AddMinutes(minute);
			}
			if (TimeRegex.IsMatch(displayTime))
			{
				var pieces = displayTime.Split(':');
				var hour = int.Parse(pieces.First());
				var minute = int.Parse(pieces.Last());
				return new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);
			}
			if (displayTime == "Nu")
			{
				return DateTime.UtcNow;
			}


			return null;
		}
	}
}
