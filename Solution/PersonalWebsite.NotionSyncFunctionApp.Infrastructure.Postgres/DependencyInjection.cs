using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Connection;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres;

public static class DependencyInjection
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var personalWebsiteDatabaseConnectionString = configuration.GetValue<string>("PersonalWebsiteDatabase");
		services.AddSingleton<IDatabaseConnectionFactory>(serviceProvider => new DatabaseConnectionFactory(personalWebsiteDatabaseConnectionString));
	}
}
