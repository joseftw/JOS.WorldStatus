using System;
using Microsoft.AspNetCore.Mvc;

namespace JOS.WorldStatus.Features.Ping
{
	[Route("")]
	[Produces(Constants.JsonContentType)]
	public class PingController : Controller
	{
		public IActionResult Index()
		{
			return new ObjectResult(new
			{
				pong = DateTime.UtcNow
			});
		}
	}
}