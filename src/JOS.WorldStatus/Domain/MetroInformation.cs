using System;
using System.Collections.Generic;
using System.Linq;
using JOS.WorldStatus.Features.Metro;

namespace JOS.WorldStatus.Domain
{
	public class MetroInformation
	{
		public MetroInformation(
			string stopAreaName,
			string displayTime,
			string destination,
			string lineNumber,
			IEnumerable<Deviation> deviations,
			DateTime? timeTabled,
			DateTime? expected
		)
		{
			StopAreaName = stopAreaName;
			Destination = destination;
			LineNumber = lineNumber;
			DisplayTime = displayTime;
			Deviations = deviations ?? Enumerable.Empty<Deviation>();
			TimeTabled = timeTabled ?? MetroHelpers.TimeTabled(displayTime);
			Expected = expected;
		}

		public DateTime? TimeTabled { get; set; }
		public DateTime? Expected { get; set; }
		public string StopAreaName { get; }
		public string Destination { get; }
		public string LineNumber { get; }
		public string DisplayTime { get; }
		public IEnumerable<Deviation> Deviations { get; }
	}
}