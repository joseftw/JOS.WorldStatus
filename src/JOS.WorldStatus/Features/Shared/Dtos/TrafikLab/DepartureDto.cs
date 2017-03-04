using System;
using System.Collections.Generic;
using System.Linq;

namespace JOS.WorldStatus.Features.Shared.Dtos.TrafikLab
{
	public class DepartureDto
	{
		public DateTime LatestUpdate { get; set; }
		public int DataAge { get; set; }
		public IEnumerable<MetroDto> Metros { get; set; } = Enumerable.Empty<MetroDto>();
		public IEnumerable<StopPointDeviationDto> StopPointDeviations { get; set; } =
			Enumerable.Empty<StopPointDeviationDto>();
	}
}