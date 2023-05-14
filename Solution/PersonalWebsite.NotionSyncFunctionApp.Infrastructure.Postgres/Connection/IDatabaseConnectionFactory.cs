using System.Data.Common;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Connection;

public interface IDatabaseConnectionFactory
{
	DbConnection GetConnection();
}