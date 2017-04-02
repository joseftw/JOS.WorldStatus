using System.IO;
using JOS.WorldStatus.Features.Authentication;
using JOS.WorldStatus.Features.Metro;
using JOS.WorldStatus.Features.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace JOS.WorldStatus
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.Enrich.FromLogContext()
				.WriteTo.RollingFile(Path.Combine(env.ContentRootPath, "log-{Date}.txt"))
				.CreateLogger();

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", true, true)
				.AddEnvironmentVariables("JOS_WorldStatus_");
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; set; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddOptions();
			services.AddCors(options =>
			{
				options.AddPolicy(Environments.Development, builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
				options.AddPolicy(Environments.Production, builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); // TODO fix.
			});
			services.Configure<MetroSettings>(Configuration.GetSection("Metro"));
			services.Configure<JwtSettings>(Configuration.GetSection("JWT"));

			services.AddSingleton<IUserStore, InMemoryUserStore>();
			services.AddSingleton<JwtTokenGenerator>();
			services.AddSingleton<GetDepartures>();
			services.AddSingleton<IGetRealTimeMetroInfoQuery, HttpGetRealTimeMetroInfoQuery>();
			services.AddMvc();
		}

		public void Configure(
			IApplicationBuilder app, 
			IHostingEnvironment env, 
			ILoggerFactory loggerFactory)
		{
			loggerFactory.AddSerilog();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(env.EnvironmentName);
			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});
			app.UseMvc();
		}
	}
}