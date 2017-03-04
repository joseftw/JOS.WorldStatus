using System.Collections.Generic;
using System.Linq;

namespace JOS.WorldStatus.Domain
{
	public class StopPointInformation
	{
		public IEnumerable<StopPointDeviation> Deviations { get; set; } = Enumerable.Empty<StopPointDeviation>();
	}
}