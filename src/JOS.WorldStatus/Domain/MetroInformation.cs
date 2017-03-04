using System.Collections.Generic;
using System.Linq;

namespace JOS.WorldStatus.Domain
{
	public class MetroInformation
	{
		public MetroInformation(
			string stopAreaName,
			string displayTime,
			string destination,
			string lineNumber,
			IEnumerable<Deviation> deviations
		)
		{
			StopAreaName = stopAreaName;
			Destination = destination;
			LineNumber = lineNumber;
			DisplayTime = displayTime;
			Deviations = deviations ?? Enumerable.Empty<Deviation>();
		}

		public string StopAreaName { get; }
		public string Destination { get; }
		public string LineNumber { get; }
		public string DisplayTime { get; }
		public IEnumerable<Deviation> Deviations { get; }
	}
}