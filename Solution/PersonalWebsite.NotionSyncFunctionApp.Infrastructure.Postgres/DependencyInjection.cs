using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalWebsite.NotionSyncFunctionApp.Application.Application;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Connection;
using PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Repository;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres;

public static class DependencyInjection
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSingleton<IBlogEntityRepository, BlogEntityRepository>();

		var personalWebsiteDatabaseConnectionString = configuration.GetValue<string>("PersonalWebsiteDatabase");
		services.AddSingleton<IDatabaseConnectionFactory>(serviceProvider => new PostgreSqlDatabaseConnectionFactory(personalWebsiteDatabaseConnectionString));
	}
}
