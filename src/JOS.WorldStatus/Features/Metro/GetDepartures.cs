using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JOS.WorldStatus.Domain;
using JOS.WorldStatus.Features.Shared.Dtos.TrafikLab;

namespace JOS.WorldStatus.Features.Metro
{
	public class GetDepartures
	{
		private readonly IGetRealTimeMetroInfoQuery _getRealTimeMetroInfoQuery;

		public GetDepartures(IGetRealTimeMetroInfoQuery getRealTimeMetroInfoQuery)
		{
			_getRealTimeMetroInfoQuery = getRealTimeMetroInfoQuery;
		}

		public async Task<Result<RealTimeMetroResult>> Handle(int siteId)
		{
			var result = await _getRealTimeMetroInfoQuery.Execute(siteId);

			if (result.StatusCode > 0)
			{
				var error = new Error
				{
					Code = result.StatusCode.ToString(),
					Message = result.Message
				};

				return Result.Fail<RealTimeMetroResult>(new List<Error> {error});
			}

			return Result.Ok(new RealTimeMetroResult
			{
				StopPointInformation = new StopPointInformation
				{
					Deviations = result.ResponseData.StopPointDeviations
						.Select(CreateStopPointDeviation)
				},
				MetroInfo =
					result.ResponseData.Metros.Select(
						x =>
							new MetroInformation(
								x.StopAreaName,
								x.DisplayTime,
								x.Destination,
								x.LineNumber,
								x.Deviations?.Select(CreateDeviation) ?? Enumerable.Empty<Deviation>()
							)),
				OldData = result.ResponseData.DataAge >= 120
			});
		}

		private static StopPointDeviation CreateStopPointDeviation(StopPointDeviationDto stopPointDeviation)
		{
			TransportMode transportMode;
			if (!Enum.TryParse(stopPointDeviation.StopInfo?.TransportMode, out transportMode))
				transportMode = TransportMode.Unspecified;
			return new StopPointDeviation(
				stopPointDeviation.StopInfo?.StopAreaName,
				transportMode,
				CreateDeviation(stopPointDeviation.Deviation)
			);
		}

		private static Deviation CreateDeviation(DeviationDto deviationDto)
		{
			if (deviationDto == null)
				return null;
			return new Deviation(
				deviationDto.ImportanceLevel,
				deviationDto.Text,
				deviationDto.Consequence
			);
		}
	}
}