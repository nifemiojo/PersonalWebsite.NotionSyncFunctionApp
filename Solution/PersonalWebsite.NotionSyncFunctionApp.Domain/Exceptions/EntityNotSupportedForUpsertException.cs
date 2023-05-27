namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres.Repository;

internal class EntityNotSupportedForUpsertException : Exception
{
	public EntityNotSupportedForUpsertException(string message) : base(message)
	{
	}
}