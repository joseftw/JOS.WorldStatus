using Microsoft.AspNetCore.Mvc;

namespace JOS.WorldStatus.Features.Ping
{
	[Route("[controller]")]
	[Produces(Constants.JsonContentType)]
	public class PingController : ControllerBase
	{
		public IActionResult Index()
		{
			return new ObjectResult(new
			{
				build = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion,
				started = Program.StartTime
			});
		}
	}
}