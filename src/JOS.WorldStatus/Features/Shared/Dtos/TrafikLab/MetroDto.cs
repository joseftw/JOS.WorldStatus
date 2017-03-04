using System;
using System.Collections.Generic;
using System.Linq;

namespace JOS.WorldStatus.Features.Shared.Dtos.TrafikLab
{
	public class MetroDto
	{
		public string GroupOfLine { get; set; }
		public string DisplayTime { get; set; }
		public string TransportMode { get; set; }
		public string LineNumber { get; set; }
		public string Destination { get; set; }
		public int JourneyDirection { get; set; }
		public string StopAreaName { get; set; }
		public int StopAreaNumber { get; set; }
		public int StopPointNumber { get; set; }
		public string StopPointDesignation { get; set; }
		public DateTime? TimeTabledDateTime { get; set; }
		public DateTime? ExpectedDateTime { get; set; }
		public int JourneyNumber { get; set; }
		public IEnumerable<DeviationDto> Deviations { get; set; } = Enumerable.Empty<DeviationDto>();
	}
}