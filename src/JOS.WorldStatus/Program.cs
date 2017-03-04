using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JOS.WorldStatus
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("hosting.json", true)
				.Build();

			var host = new WebHostBuilder()
				.UseKestrel()
				.UseConfiguration(config)
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseStartup<Startup>()
				.Build();

			var services = (IServiceScopeFactory) host.Services.GetService(typeof(IServiceScopeFactory));
			using (var scope = services.CreateScope())
			{
				var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
				logger.LogInformation(LogEvents.ProgramHostBeforeRun, "Host built, calling host.Run()");
			}

			host.Run();
		}
	}
}