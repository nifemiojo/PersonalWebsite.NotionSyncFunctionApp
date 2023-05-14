using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Connection;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres;

public static class DependencyInjection
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var personalWebsiteConnectionString = configuration.GetSection("ConnectionStrings").Get<string>();
		services.AddSingleton<IDatabaseConnectionFactory>(serviceProvider => new DatabaseConnectionFactory(personalWebsiteConnectionString));
	}
}
