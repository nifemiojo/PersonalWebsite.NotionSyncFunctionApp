namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres;

internal interface IDatabase
{
	Task BulkUpsertAsync<T>(string command, object param);
}