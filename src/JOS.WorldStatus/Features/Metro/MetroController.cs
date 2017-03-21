using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JOS.WorldStatus.Features.Metro
{
	[Route(Constants.ApiPrefix + "/[controller]")]
	[Produces(Constants.JsonContentType)]
	public class MetroController : ControllerBase
	{
		private readonly GetDepartures _getDepartures;

		public MetroController(GetDepartures getDepartures)
		{
			_getDepartures = getDepartures;
		}

		[Route("{siteId}")]
		public async Task<IActionResult> Index(int siteId)
		{
			var result = await this._getDepartures.Handle(siteId);

			if (result.IsFailure)
			{
				var errors = result.Errors;
				var errorResponse = new ObjectResult(errors) {StatusCode = 500};
				return errorResponse;
			}

			return new ObjectResult(result.Value);
		}
	}
}
