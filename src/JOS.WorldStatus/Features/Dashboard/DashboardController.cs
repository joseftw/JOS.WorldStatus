using Microsoft.AspNetCore.Mvc;


namespace JOS.WorldStatus.Features.Dashboard
{
	[Route("")]
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View("~/Features/Dashboard/index.html");
		}
	}
}
