using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model;
using System;

namespace WebAPI
{
	public class Startup
	{
		private IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddHealthChecks();
			services.AddGiraModel(new GitHubTasksDataSource(new GiraGitHubFacade(Configuration["Pem"])));
			services
				.AddAuthentication("BasicAuthentication")
				.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseHealthChecks(Configuration["HealthCheckPath"]);
			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
