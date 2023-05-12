using Dapper;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres;

internal interface IDatabase
{
	Task SomeStoredProcedureAsync<T>(DynamicParameters parameters);
}