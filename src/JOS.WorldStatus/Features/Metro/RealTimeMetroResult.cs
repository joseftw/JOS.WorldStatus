using System.Collections.Generic;
using System.Linq;
using JOS.WorldStatus.Domain;

namespace JOS.WorldStatus.Features.Metro
{
	public class RealTimeMetroResult
	{
		public bool OldData { get; set; }
		public StopPointInformation StopPointInformation { get; set; }
		public IEnumerable<MetroInformation> MetroInfo { get; set; } = Enumerable.Empty<MetroInformation>();
	}
}