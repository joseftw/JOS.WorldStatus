using System.Threading.Tasks;
using JOS.WorldStatus.Features.Shared.Dtos.TrafikLab;

namespace JOS.WorldStatus.Features.Metro
{
	public interface IGetRealTimeMetroInfoQuery
	{
		Task<TrafikLabResponseDto<DepartureDto>> Execute(int siteId);
	}
}