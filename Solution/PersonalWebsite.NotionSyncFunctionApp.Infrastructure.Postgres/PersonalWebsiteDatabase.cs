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

	public async Task NonQueryOperationAsync<T>(DynamicParameters parameters)
	{
		await _db.ExecuteAsync("SP Name", parameters, commandType: CommandType.StoredProcedure);
	}
}