using System.Threading.Tasks;
using JOS.WorldStatus.Features.Shared.Dtos.TrafikLab;

namespace JOS.WorldStatus.Domain
{
	public interface IGetRealTimeMetroInfoQuery
	{
		Task<TrafikLabResponseDto<DepartureDto>> Execute(int siteId);
	}
}