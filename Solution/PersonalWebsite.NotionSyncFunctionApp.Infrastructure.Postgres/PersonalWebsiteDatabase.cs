using Dapper;
using Npgsql;
using System.Data;

namespace PersonalWebsite.NotionSyncFunctionApp.Infrastructure.Postgres;

class PersonalWebsiteDatabase : IDatabase
{
	private readonly IDbConnection _db;

	public PersonalWebsiteDatabase()
	{
		_db = new NpgsqlConnection("placeholder");
	}

	public async Task BulkUpsertAsync<T>(string command, object param)
	{
		await _db.ExecuteAsync(command, param);
	}
}