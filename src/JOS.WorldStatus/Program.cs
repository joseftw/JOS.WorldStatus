using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace JOS.WorldStatus
{
	public class Program
	{
		public static DateTime StartTime;
		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseStartup<Startup>()
				.UseUrls("http://localhost:5521")
				.Build();

			var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
			using (var scope = services.CreateScope())
			{
				var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
				logger.LogInformation(LogEvents.ProgramHostBeforeRun, "Host built, calling host.Run()");
			}

			StartTime = DateTime.UtcNow;

			host.Run();
		}
	}
}